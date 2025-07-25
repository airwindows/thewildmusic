using Godot;
using System;
public partial class RangeUpdate : TextEdit
{
	public void Update()
	{
		int lowest = GetNode<TextEdit>("/root/Main/PianoRoll/Lowest").Text.ToInt();
		int belt = GetNode<TextEdit>("/root/Main/PianoRoll/Belt").Text.ToInt();
		int falsetto = GetNode<TextEdit>("/root/Main/PianoRoll/Falsetto").Text.ToInt();
		
		if (lowest < 0) { lowest = 0; GetNode<TextEdit>("/root/Main/PianoRoll/Lowest").Text = lowest.ToString(); }
		if (lowest > 127) { lowest = 127; GetNode<TextEdit>("/root/Main/PianoRoll/Lowest").Text = lowest.ToString(); }
		if (falsetto < 0) { falsetto = 0; GetNode<TextEdit>("/root/Main/PianoRoll/Falsetto").Text = falsetto.ToString(); }
		if (falsetto > 127) { falsetto = 127; GetNode<TextEdit>("/root/Main/PianoRoll/Falsetto").Text = falsetto.ToString(); }
		if (belt < 0) { belt = 0; GetNode<TextEdit>("/root/Main/PianoRoll/Belt").Text = belt.ToString(); }
		if (belt > 127) { belt = 127; GetNode<TextEdit>("/root/Main/PianoRoll/Belt").Text = belt.ToString(); }
		
		GetNode<ScrollBar>("/root/Main/PianoRoll/ScrollH").MinValue = lowest;
		GetNode<ScrollBar>("/root/Main/PianoRoll/ScrollH").MaxValue = falsetto-19;
		GetNode<ScrollBar>("/root/Main/PianoRoll/ScrollH").Page = 12.0;

		
	}
}
