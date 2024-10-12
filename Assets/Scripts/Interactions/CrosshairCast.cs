using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairCast : MonoBehaviour, IGameState
{
    private CrosshairTypes crosshairType;
    private CrosshairTypes CrosshairType {
        set
        {
            crosshairType = value;
            if (crosshairType == CrosshairTypes.Hidden) crosshair.enabled = false;
            else
            {
                crosshair.enabled = true;
                crosshair.texture = crosshairs[(int)crosshairType];
            }
        }
    }

    [SerializeField] [Tooltip(Cursed.crosshairsTooltip)] private Texture[] crosshairs = new Texture[Cursed.numberOfCrosshairs]; //Mmmmmmhm lovely dynamic tooltip
    
    private RawImage crosshair;

    [SerializeField] [Tooltip("Maximum range of the raycast")] private float maximumRange = 2.5f;
    private new Camera camera;
    private RaycastHit raycastHit;
    public PickupableObject pickupableObject { get; private set; }
    public IInteractable interactable { get; private set; }

    private void Awake()
    {
        crosshair = GetComponent<RawImage>();
        camera = Camera.main;
        GameManager.Subscribe(this);
    }
    
    public void OnGameStateChanged(GameState gameState) => crosshairType = gameState == GameState.Playing ? CrosshairTypes.Normal : CrosshairTypes.Hidden;

    private void FixedUpdate()
    {
        if (GameManager.gameState != GameState.Playing) return; //Only run this code if the gameState is playing

        Physics.SphereCast(camera.transform.position, 0.1f, camera.transform.forward, out raycastHit, maximumRange, ~(1 << 6) /* Ignore the player */);

        GameObject raycastObject = raycastHit.transform?.gameObject; //I only need to look for the game object of the spherecast so this is here
    
        if (raycastObject?.layer == 3)
        {
            pickupableObject = raycastObject?.gameObject.GetComponent<PickupableObject>(); //This should always return a pickupable object since they should all be exclusively on layer 3
            if (pickupableObject != null) CrosshairType = CrosshairTypes.Pickup; //If there is a pickupable object, set the crosshair type to be the icon for picking up and object
            return;
        }
        pickupableObject = null; //Make sure this is null if the check for a pickupable object failed

        interactable = raycastObject?.gameObject.GetComponent<IInteractable>(); //Try to find an interactable object
        CrosshairType = interactable == null ? CrosshairTypes.Normal : CrosshairTypes.Interaction; //If there is an interactable object, set the crosshair to its icon. Otherwise use the normal crosshair
    }

    #if UNITY_EDITOR
    private void OnValidate() //Horrifically cursed function to update the crosshairsTooltip const so that I can have a dynamic tooltip
    {
        FileStream stream = File.Create($"{Application.dataPath}\\CrosshairsTooltip.cs");
        StreamWriter writer = new(stream);

        string[] crosshairTypes = Enum.GetNames(typeof(CrosshairTypes));
        string tooltip = "Different crosshairs depending on what is interactable in what way:";
        for (int i = 0; i < crosshairTypes.Length; i++) tooltip += $"\\n{i}: {crosshairTypes[i]}";

        string classText = $"public class Cursed\n{{\n\tpublic const string crosshairsTooltip = \"{tooltip}\";\n\tpublic const int numberOfCrosshairs = {crosshairTypes.Length - 1};\n}}";
        writer.Write(classText);

        writer.Close();
        stream.Close();
    }
    #endif
}

public enum CrosshairTypes
{
    Normal,
    Interaction,
    Pickup,
    Hidden = int.MaxValue,
}