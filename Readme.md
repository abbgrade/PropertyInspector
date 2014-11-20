# Property Inspector

*PropertyInspector* is a improved *DefaultInspector* for the [*Unity3d* game engine](http://unity3d.com). It is tested with *Unity 4* and may work with older versions.

For debug purposes you may want to view the values of of properties of unserialized fields of your *MonoBehaviors*.

## License

This is free software and it is provided under MIT license.

## Installation

### From Unitypackage

Select in the Menu "Assets/Import Package/Custom Package"
Select your local copy of "PropertyInspector.unitypackage"

### From GitHub

*Instruction will follow.*

## Usage

### Show Properties in the Inspector

Just add the attribute *ShowInInspector* to the property which should be displayed.
Note that the getter must be public.

#### Example

	[ShowInInspector]
	public Vector3 GlobalPosition
	{
		Get { return this.transform.position; }
	}

### Custom Inspectors

Instead of inherit from *UnityEditor.Editor* inherit from *PropertyInspector.Editor*.
Don't forget to call *base.OnInspectorGUI* if you override *OnInspectorGUI*.

#### Example

	using UnityEditor;
	using UnityEngine;

	[CustomEditor(typeof(CustomBehavior))]
	public class CustomInspector : PropertyInspector.Editor
	{
	    public override void OnInspectorGUI()
	    {
	        base.OnInspectorGUI();

	        if (GUILayout.Button("Do Fun"))
	        {
	            Debug.Log("Fun");
	        }
	    }
	}

## Contribution & Support

If you have a question or contribution to the *PropertyInspector* visit our [GitHub Project](https://github.com/abbgrade/PropertyInspector).