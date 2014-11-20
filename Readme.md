# Property Inspector

*PropertyInspector* is a improved *DefaultInspector* for the [*Unity3d* game engine](http://unity3d.com). It's tested with *Unity 4* and may work with older versions.

For debug purposes you may want to view the values of properties and unserialized fields of your *MonoBehaviors*. With the *PropertyInspector* this is very easy.

## License

This is free software and it is provided under MIT license, that is distributed with this package.

## Contribution & Support

If you have a question or contribution to the *PropertyInspector* visit our [GitHub Project](https://github.com/abbgrade/PropertyInspector).

To build the readme on *Windows* and *VisualStudio* you have to install [Pandoc](johnmacfarlane.net/pandoc) and [MiKTeX](http://miktex.org/).

## Installation

### From Unitypackage

Select in the Menu "Assets/Import Package/Custom Package"
Select your local copy of "PropertyInspector.unitypackage"

### From GitHub

Download the [sources](https://github.com/abbgrade/PropertyInspector/archive/master.zip) from *Github*.
Unpack *Editor.cs* *ShowInInspector.cs* to your projects *Assets* directory.

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