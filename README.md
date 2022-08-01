# Tooltippery

This is a mod for NeosVR that enables the functionality to show hober tooltips on UI elements.  
By default, the mod doesn't add any specific tooltips on its own but other players can set their items up in a way where they get tooltips by default.

## Adding Tooltips

### For Builders

To add a tooltip to a UIX Button, simply put a Comment component (Uncategorized -> Comment) on the same slot as the Button component and set its Text to "TooltipperyLabel:Example Text", where Example Text would be replaced with what you want it to show on the tooltip.

### For Modders

If you are making a mod that needs more than just a button with associated tooltip text, this mod provides multiple things:  
1. The Tooltippery class has a property called labelProviders.  
This is of type List<Func<Button, ButtonEventData, string>>, and holds a list of functions which, given a Button and ButtonEventData object, can return any string they like, indicating that that should be a label for the Button in question. These are consulted every time the player hovers over a Button.
2. If you wish to manually open tooltips for some other reason, the Tooltippery class provides both a showTooltip() and a hideTooltip() function.  
showTooltip()  takes a string, Slot and float3, telling it where to parent the new tooltip and what text to put on it.  
It then returns a Tooltip object which can be passed to hideTooltip() to close the tooltip again.  
The Tooltip object also has a setText() function, which you can call to alter the text on the tooltip after it has been created.
