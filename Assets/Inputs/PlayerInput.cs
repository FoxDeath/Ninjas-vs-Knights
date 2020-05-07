// GENERATED AUTOMATICALLY FROM 'Assets/Inputs/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Ninja"",
            ""id"": ""3ca94b9f-d444-4775-ba40-48dfbfe5135c"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""3d8e595e-6322-46c8-aebf-d75597715fe5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""162691f4-2fe3-44a8-9b80-6b661fed831f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""19193a2f-8a8d-4354-9b91-d472e3cc1484"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""0f35ab6f-75df-461c-9f0e-051d7a62d3a9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Grapling Hook"",
                    ""type"": ""Button"",
                    ""id"": ""4c0a85ae-4079-45df-bd15-ebea32cffde3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""8f4b225b-234d-4413-b343-145b6a96b3c3"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""7bddba4d-f239-433c-90a5-a0818b51b4f5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""2d82f41a-d418-47d2-b863-dc6db34d7571"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LookPosition"",
                    ""type"": ""Value"",
                    ""id"": ""81145bce-9fd2-4242-b677-dde3d491f35f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stimpack"",
                    ""type"": ""Button"",
                    ""id"": ""e5f7795b-df4e-4ad6-b648-00c744423746"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Kunai"",
                    ""type"": ""Button"",
                    ""id"": ""e12a980b-a075-4bb0-abbc-c1705ece4a02"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fd911d69-d345-4717-a435-f39617209e95"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8796cef5-c631-4910-af80-ac53573bde4b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9a01ef79-628d-41d4-a334-b486016fad31"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4cf5353d-fe72-4883-9ab8-2d70f4afc3ac"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f1cd8abd-f175-4e06-9479-5083ad00e0f6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""48d98b1b-288a-4c51-b6a0-88b44172b72f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""96e76c42-87f1-48ef-b01d-3cbe3668f47f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b4df8866-84ea-4aae-b303-983e451ea9c1"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36796939-65e8-4c7c-94c2-e9452eb1a729"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Grapling Hook"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b2f33c4b-b591-4aef-9388-d37f9086e52d"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72414897-3f78-460b-b543-7303002c264d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1a826f7-144e-4db0-a669-69d5ff4a9511"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11b5e546-2d1e-42fe-82e9-01fda1e20cd2"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""LookPosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e651270-5f3e-4d4d-94ab-53fe2b96355d"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stimpack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""96c7538a-bbbc-4286-b80c-6fda0f83b888"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Kunai"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Knight"",
            ""id"": ""75620fe3-3537-4cbf-beb0-34235a9b816d"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""f3881dd5-c9bc-42b4-9b39-b25ffa9635cb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""13e8d25b-0bfe-41a5-88e0-0cac72d74e1e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""4c9b6429-bd9c-465c-9e28-b4549d5f0997"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JetpackDash"",
                    ""type"": ""Button"",
                    ""id"": ""3dd231e9-759b-4ebc-90db-879e87f7c9d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""8f80fc03-3940-4607-8564-c3746f1daabe"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""PauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""42ee6374-1e9b-494c-b053-e59af83cb8c1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchWeapon"",
                    ""type"": ""Value"",
                    ""id"": ""830f39fc-f603-49a2-a7c9-77763e89440a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""7a0fddce-dc0a-4c4b-864a-444ff849b4c4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""JatpackCharge"",
                    ""type"": ""Button"",
                    ""id"": ""99442510-9d27-4c80-b1b4-d328aa5fb7d1"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""AOEAttack"",
                    ""type"": ""Button"",
                    ""id"": ""1af5e068-4066-42de-a401-6cb0c33b7e98"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Stimpack"",
                    ""type"": ""Button"",
                    ""id"": ""bb120c0b-518c-4c39-93da-54769bc7e018"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ac083a0-9a04-4ec2-9919-cd3caa11dc8b"",
                    ""path"": ""<Pointer>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""cee7bd15-1089-4907-8244-80004dceb87f"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4a899fcd-3cbb-4a71-a4bf-4d4c4ca8919d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""efb79e6a-f230-428e-811c-baee3d81813b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1098bb7a-40c1-4972-aeed-8855ff53c7c0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9b82cc51-bd14-45b5-82d2-3304e22062fc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""12564c05-902f-44dc-a813-84b69ad9a1a2"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ed5a03e-caef-41b5-bb92-dc53e9570112"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Hold(duration=0.001)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8020a29f-758d-4ed9-af8c-710d9b195ee0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""MultiTap(tapDelay=0.2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""JetpackDash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ee7f151-61fe-4ec0-b48b-85f827ac354c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""PauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e3d03654-08cf-4fb0-a46d-0f141b8a841f"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SwitchWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""92e444f2-14b0-4aab-8b6c-3122c3a7f797"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": ""Hold(duration=0.01)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""688a4861-a63f-4ff4-9c7c-f7dee30a44df"",
                    ""path"": ""<Keyboard>/#(F)"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""JatpackCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3626ab9-cecb-4589-ad6f-5bbc8b4ea357"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AOEAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6224f8c-cffb-4ed7-ab94-6218498bc3e5"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Stimpack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Weapon"",
            ""id"": ""51b287e4-ab2b-4ff9-a3ba-ddf9f2390903"",
            ""actions"": [
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d0177fad-de37-4d4c-ae08-b6e1b0f69e12"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""ScopeZoom"",
                    ""type"": ""Button"",
                    ""id"": ""9aab2f80-be9d-4b02-955a-6f6400162694"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Scope"",
                    ""type"": ""Button"",
                    ""id"": ""0a2aa2c1-ecb7-4c0a-9785-246cf1f17896"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.001)""
                },
                {
                    ""name"": ""Fire"",
                    ""type"": ""Button"",
                    ""id"": ""51d6c75b-66c3-48cb-afbc-0ffe21d26190"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press(pressPoint=0.001),Hold(duration=0.001,pressPoint=0.001)""
                },
                {
                    ""name"": ""FireCharge"",
                    ""type"": ""Button"",
                    ""id"": ""9c1c94d4-b000-4f33-adb1-a68149a3cf69"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Charge"",
                    ""type"": ""Button"",
                    ""id"": ""6ce10f68-eb44-4e14-a5a1-8b20806a2ce4"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Hold""
                },
                {
                    ""name"": ""Grenade"",
                    ""type"": ""Button"",
                    ""id"": ""d079cde1-3934-4c5a-88f9-a1ed4c4aae73"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""51c3dce5-a881-4772-8d17-5c7d97cd09a4"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Fire"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ec654fb-0bc9-414f-bc80-6cd0ce6b0523"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Scope"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""667b968a-88e7-44d4-84cd-6b1779f56b7d"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""ScopeZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""51e931db-44aa-4447-93e0-f0ad9070b0dc"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49e484b3-7026-4caf-9155-60f61d2d8325"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stimpack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""917851e7-84f2-448b-b8c0-cfa6a919d067"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Charge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c94b8a8-d564-42b3-805b-6e283b6c145f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""FireCharge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c6e8946-7a92-4704-82a8-31b76c3eaf79"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Grenade"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Ninja
        m_Ninja = asset.FindActionMap("Ninja", throwIfNotFound: true);
        m_Ninja_Look = m_Ninja.FindAction("Look", throwIfNotFound: true);
        m_Ninja_Movement = m_Ninja.FindAction("Movement", throwIfNotFound: true);
        m_Ninja_Jump = m_Ninja.FindAction("Jump", throwIfNotFound: true);
        m_Ninja_Sprint = m_Ninja.FindAction("Sprint", throwIfNotFound: true);
        m_Ninja_GraplingHook = m_Ninja.FindAction("Grapling Hook", throwIfNotFound: true);
        m_Ninja_Crouch = m_Ninja.FindAction("Crouch", throwIfNotFound: true);
        m_Ninja_PauseMenu = m_Ninja.FindAction("PauseMenu", throwIfNotFound: true);
        m_Ninja_SwitchWeapon = m_Ninja.FindAction("SwitchWeapon", throwIfNotFound: true);
        m_Ninja_LookPosition = m_Ninja.FindAction("LookPosition", throwIfNotFound: true);
        m_Ninja_Stimpack = m_Ninja.FindAction("Stimpack", throwIfNotFound: true);
        m_Ninja_Kunai = m_Ninja.FindAction("Kunai", throwIfNotFound: true);
        // Knight
        m_Knight = asset.FindActionMap("Knight", throwIfNotFound: true);
        m_Knight_Look = m_Knight.FindAction("Look", throwIfNotFound: true);
        m_Knight_Movement = m_Knight.FindAction("Movement", throwIfNotFound: true);
        m_Knight_Sprint = m_Knight.FindAction("Sprint", throwIfNotFound: true);
        m_Knight_JetpackDash = m_Knight.FindAction("JetpackDash", throwIfNotFound: true);
        m_Knight_Jump = m_Knight.FindAction("Jump", throwIfNotFound: true);
        m_Knight_PauseMenu = m_Knight.FindAction("PauseMenu", throwIfNotFound: true);
        m_Knight_SwitchWeapon = m_Knight.FindAction("SwitchWeapon", throwIfNotFound: true);
        m_Knight_Crouch = m_Knight.FindAction("Crouch", throwIfNotFound: true);
        m_Knight_JatpackCharge = m_Knight.FindAction("JatpackCharge", throwIfNotFound: true);
        m_Knight_AOEAttack = m_Knight.FindAction("AOEAttack", throwIfNotFound: true);
        m_Knight_Stimpack = m_Knight.FindAction("Stimpack", throwIfNotFound: true);
        // Weapon
        m_Weapon = asset.FindActionMap("Weapon", throwIfNotFound: true);
        m_Weapon_Reload = m_Weapon.FindAction("Reload", throwIfNotFound: true);
        m_Weapon_ScopeZoom = m_Weapon.FindAction("ScopeZoom", throwIfNotFound: true);
        m_Weapon_Scope = m_Weapon.FindAction("Scope", throwIfNotFound: true);
        m_Weapon_Fire = m_Weapon.FindAction("Fire", throwIfNotFound: true);
        m_Weapon_FireCharge = m_Weapon.FindAction("FireCharge", throwIfNotFound: true);
        m_Weapon_Charge = m_Weapon.FindAction("Charge", throwIfNotFound: true);
        m_Weapon_Grenade = m_Weapon.FindAction("Grenade", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Ninja
    private readonly InputActionMap m_Ninja;
    private INinjaActions m_NinjaActionsCallbackInterface;
    private readonly InputAction m_Ninja_Look;
    private readonly InputAction m_Ninja_Movement;
    private readonly InputAction m_Ninja_Jump;
    private readonly InputAction m_Ninja_Sprint;
    private readonly InputAction m_Ninja_GraplingHook;
    private readonly InputAction m_Ninja_Crouch;
    private readonly InputAction m_Ninja_PauseMenu;
    private readonly InputAction m_Ninja_SwitchWeapon;
    private readonly InputAction m_Ninja_LookPosition;
    private readonly InputAction m_Ninja_Stimpack;
    private readonly InputAction m_Ninja_Kunai;
    public struct NinjaActions
    {
        private @PlayerInput m_Wrapper;
        public NinjaActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Ninja_Look;
        public InputAction @Movement => m_Wrapper.m_Ninja_Movement;
        public InputAction @Jump => m_Wrapper.m_Ninja_Jump;
        public InputAction @Sprint => m_Wrapper.m_Ninja_Sprint;
        public InputAction @GraplingHook => m_Wrapper.m_Ninja_GraplingHook;
        public InputAction @Crouch => m_Wrapper.m_Ninja_Crouch;
        public InputAction @PauseMenu => m_Wrapper.m_Ninja_PauseMenu;
        public InputAction @SwitchWeapon => m_Wrapper.m_Ninja_SwitchWeapon;
        public InputAction @LookPosition => m_Wrapper.m_Ninja_LookPosition;
        public InputAction @Stimpack => m_Wrapper.m_Ninja_Stimpack;
        public InputAction @Kunai => m_Wrapper.m_Ninja_Kunai;
        public InputActionMap Get() { return m_Wrapper.m_Ninja; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NinjaActions set) { return set.Get(); }
        public void SetCallbacks(INinjaActions instance)
        {
            if (m_Wrapper.m_NinjaActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnJump;
                @Sprint.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSprint;
                @GraplingHook.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @GraplingHook.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @GraplingHook.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnGraplingHook;
                @Crouch.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnCrouch;
                @PauseMenu.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnPauseMenu;
                @SwitchWeapon.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnSwitchWeapon;
                @LookPosition.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLookPosition;
                @LookPosition.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLookPosition;
                @LookPosition.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnLookPosition;
                @Stimpack.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnStimpack;
                @Stimpack.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnStimpack;
                @Stimpack.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnStimpack;
                @Kunai.started -= m_Wrapper.m_NinjaActionsCallbackInterface.OnKunai;
                @Kunai.performed -= m_Wrapper.m_NinjaActionsCallbackInterface.OnKunai;
                @Kunai.canceled -= m_Wrapper.m_NinjaActionsCallbackInterface.OnKunai;
            }
            m_Wrapper.m_NinjaActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @GraplingHook.started += instance.OnGraplingHook;
                @GraplingHook.performed += instance.OnGraplingHook;
                @GraplingHook.canceled += instance.OnGraplingHook;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @LookPosition.started += instance.OnLookPosition;
                @LookPosition.performed += instance.OnLookPosition;
                @LookPosition.canceled += instance.OnLookPosition;
                @Stimpack.started += instance.OnStimpack;
                @Stimpack.performed += instance.OnStimpack;
                @Stimpack.canceled += instance.OnStimpack;
                @Kunai.started += instance.OnKunai;
                @Kunai.performed += instance.OnKunai;
                @Kunai.canceled += instance.OnKunai;
            }
        }
    }
    public NinjaActions @Ninja => new NinjaActions(this);

    // Knight
    private readonly InputActionMap m_Knight;
    private IKnightActions m_KnightActionsCallbackInterface;
    private readonly InputAction m_Knight_Look;
    private readonly InputAction m_Knight_Movement;
    private readonly InputAction m_Knight_Sprint;
    private readonly InputAction m_Knight_JetpackDash;
    private readonly InputAction m_Knight_Jump;
    private readonly InputAction m_Knight_PauseMenu;
    private readonly InputAction m_Knight_SwitchWeapon;
    private readonly InputAction m_Knight_Crouch;
    private readonly InputAction m_Knight_JatpackCharge;
    private readonly InputAction m_Knight_AOEAttack;
    private readonly InputAction m_Knight_Stimpack;
    public struct KnightActions
    {
        private @PlayerInput m_Wrapper;
        public KnightActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Knight_Look;
        public InputAction @Movement => m_Wrapper.m_Knight_Movement;
        public InputAction @Sprint => m_Wrapper.m_Knight_Sprint;
        public InputAction @JetpackDash => m_Wrapper.m_Knight_JetpackDash;
        public InputAction @Jump => m_Wrapper.m_Knight_Jump;
        public InputAction @PauseMenu => m_Wrapper.m_Knight_PauseMenu;
        public InputAction @SwitchWeapon => m_Wrapper.m_Knight_SwitchWeapon;
        public InputAction @Crouch => m_Wrapper.m_Knight_Crouch;
        public InputAction @JatpackCharge => m_Wrapper.m_Knight_JatpackCharge;
        public InputAction @AOEAttack => m_Wrapper.m_Knight_AOEAttack;
        public InputAction @Stimpack => m_Wrapper.m_Knight_Stimpack;
        public InputActionMap Get() { return m_Wrapper.m_Knight; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KnightActions set) { return set.Get(); }
        public void SetCallbacks(IKnightActions instance)
        {
            if (m_Wrapper.m_KnightActionsCallbackInterface != null)
            {
                @Look.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnLook;
                @Movement.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnMovement;
                @Sprint.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @Sprint.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnSprint;
                @JetpackDash.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @JetpackDash.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @JetpackDash.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJetpackDash;
                @Jump.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJump;
                @PauseMenu.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnPauseMenu;
                @PauseMenu.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnPauseMenu;
                @SwitchWeapon.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnSwitchWeapon;
                @SwitchWeapon.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnSwitchWeapon;
                @Crouch.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnCrouch;
                @JatpackCharge.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnJatpackCharge;
                @JatpackCharge.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnJatpackCharge;
                @JatpackCharge.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnJatpackCharge;
                @AOEAttack.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnAOEAttack;
                @AOEAttack.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnAOEAttack;
                @AOEAttack.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnAOEAttack;
                @Stimpack.started -= m_Wrapper.m_KnightActionsCallbackInterface.OnStimpack;
                @Stimpack.performed -= m_Wrapper.m_KnightActionsCallbackInterface.OnStimpack;
                @Stimpack.canceled -= m_Wrapper.m_KnightActionsCallbackInterface.OnStimpack;
            }
            m_Wrapper.m_KnightActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Sprint.started += instance.OnSprint;
                @Sprint.performed += instance.OnSprint;
                @Sprint.canceled += instance.OnSprint;
                @JetpackDash.started += instance.OnJetpackDash;
                @JetpackDash.performed += instance.OnJetpackDash;
                @JetpackDash.canceled += instance.OnJetpackDash;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @PauseMenu.started += instance.OnPauseMenu;
                @PauseMenu.performed += instance.OnPauseMenu;
                @PauseMenu.canceled += instance.OnPauseMenu;
                @SwitchWeapon.started += instance.OnSwitchWeapon;
                @SwitchWeapon.performed += instance.OnSwitchWeapon;
                @SwitchWeapon.canceled += instance.OnSwitchWeapon;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @JatpackCharge.started += instance.OnJatpackCharge;
                @JatpackCharge.performed += instance.OnJatpackCharge;
                @JatpackCharge.canceled += instance.OnJatpackCharge;
                @AOEAttack.started += instance.OnAOEAttack;
                @AOEAttack.performed += instance.OnAOEAttack;
                @AOEAttack.canceled += instance.OnAOEAttack;
                @Stimpack.started += instance.OnStimpack;
                @Stimpack.performed += instance.OnStimpack;
                @Stimpack.canceled += instance.OnStimpack;
            }
        }
    }
    public KnightActions @Knight => new KnightActions(this);

    // Weapon
    private readonly InputActionMap m_Weapon;
    private IWeaponActions m_WeaponActionsCallbackInterface;
    private readonly InputAction m_Weapon_Reload;
    private readonly InputAction m_Weapon_ScopeZoom;
    private readonly InputAction m_Weapon_Scope;
    private readonly InputAction m_Weapon_Fire;
    private readonly InputAction m_Weapon_FireCharge;
    private readonly InputAction m_Weapon_Charge;
    private readonly InputAction m_Weapon_Grenade;
    public struct WeaponActions
    {
        private @PlayerInput m_Wrapper;
        public WeaponActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Reload => m_Wrapper.m_Weapon_Reload;
        public InputAction @ScopeZoom => m_Wrapper.m_Weapon_ScopeZoom;
        public InputAction @Scope => m_Wrapper.m_Weapon_Scope;
        public InputAction @Fire => m_Wrapper.m_Weapon_Fire;
        public InputAction @FireCharge => m_Wrapper.m_Weapon_FireCharge;
        public InputAction @Charge => m_Wrapper.m_Weapon_Charge;
        public InputAction @Grenade => m_Wrapper.m_Weapon_Grenade;
        public InputActionMap Get() { return m_Wrapper.m_Weapon; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(WeaponActions set) { return set.Get(); }
        public void SetCallbacks(IWeaponActions instance)
        {
            if (m_Wrapper.m_WeaponActionsCallbackInterface != null)
            {
                @Reload.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @Reload.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @Reload.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnReload;
                @ScopeZoom.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScopeZoom;
                @ScopeZoom.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScopeZoom;
                @ScopeZoom.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScopeZoom;
                @Scope.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScope;
                @Scope.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScope;
                @Scope.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnScope;
                @Fire.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @Fire.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @Fire.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFire;
                @FireCharge.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireCharge;
                @FireCharge.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireCharge;
                @FireCharge.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnFireCharge;
                @Charge.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnCharge;
                @Charge.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnCharge;
                @Charge.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnCharge;
                @Grenade.started -= m_Wrapper.m_WeaponActionsCallbackInterface.OnGrenade;
                @Grenade.performed -= m_Wrapper.m_WeaponActionsCallbackInterface.OnGrenade;
                @Grenade.canceled -= m_Wrapper.m_WeaponActionsCallbackInterface.OnGrenade;
            }
            m_Wrapper.m_WeaponActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Reload.started += instance.OnReload;
                @Reload.performed += instance.OnReload;
                @Reload.canceled += instance.OnReload;
                @ScopeZoom.started += instance.OnScopeZoom;
                @ScopeZoom.performed += instance.OnScopeZoom;
                @ScopeZoom.canceled += instance.OnScopeZoom;
                @Scope.started += instance.OnScope;
                @Scope.performed += instance.OnScope;
                @Scope.canceled += instance.OnScope;
                @Fire.started += instance.OnFire;
                @Fire.performed += instance.OnFire;
                @Fire.canceled += instance.OnFire;
                @FireCharge.started += instance.OnFireCharge;
                @FireCharge.performed += instance.OnFireCharge;
                @FireCharge.canceled += instance.OnFireCharge;
                @Charge.started += instance.OnCharge;
                @Charge.performed += instance.OnCharge;
                @Charge.canceled += instance.OnCharge;
                @Grenade.started += instance.OnGrenade;
                @Grenade.performed += instance.OnGrenade;
                @Grenade.canceled += instance.OnGrenade;
            }
        }
    }
    public WeaponActions @Weapon => new WeaponActions(this);
    private int m_KeyboardandMouseSchemeIndex = -1;
    public InputControlScheme KeyboardandMouseScheme
    {
        get
        {
            if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
            return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface INinjaActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnGraplingHook(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnLookPosition(InputAction.CallbackContext context);
        void OnStimpack(InputAction.CallbackContext context);
        void OnKunai(InputAction.CallbackContext context);
    }
    public interface IKnightActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnJetpackDash(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnPauseMenu(InputAction.CallbackContext context);
        void OnSwitchWeapon(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnJatpackCharge(InputAction.CallbackContext context);
        void OnAOEAttack(InputAction.CallbackContext context);
        void OnStimpack(InputAction.CallbackContext context);
    }
    public interface IWeaponActions
    {
        void OnReload(InputAction.CallbackContext context);
        void OnScopeZoom(InputAction.CallbackContext context);
        void OnScope(InputAction.CallbackContext context);
        void OnFire(InputAction.CallbackContext context);
        void OnFireCharge(InputAction.CallbackContext context);
        void OnCharge(InputAction.CallbackContext context);
        void OnGrenade(InputAction.CallbackContext context);
    }
}
