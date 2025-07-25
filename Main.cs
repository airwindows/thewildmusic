using Godot;

namespace TheWildMusic;
public partial class Main : Node2D
{
	private Image _display;
	private Image _keysDisplay;
	private Image _cropDisplay;
	private ImageTexture _displayTexture;

	private string[][] chords = new string[12][];
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		chords[0] = ["C", "Dm", "Em", "F", "G7", "Am", "Bm"]; //C
		chords[1] = ["G", "Am", "Bm", "C", "D7", "Em", "F#m"]; //G
		chords[2] = ["D", "Em", "F#m", "G", "A7", "Bm", "C#m"]; //D
		chords[3] = ["A", "Bm", "C#m", "D", "E7", "F#m", "G#m"]; //A
		chords[4] = ["E", "F#m", "G#m", "A", "B7", "C#m", "D#m"]; //E
		chords[5] = ["B", "C#m", "D#m", "E", "F#7", "G#m", "A#m"]; //B
		chords[6] = ["F#", "G#m", "A#m", "B", "Db7", "Ebm", "Fm"]; //F#
		chords[7] = ["Db", "Ebm", "Fm", "Gb", "Ab7", "Bbm", "Cm"]; //Db
		chords[8] = ["Ab", "Bbm", "Cm", "Db", "Eb7", "Fm", "Gm"]; //Ab
		chords[9] = ["Eb", "Fm", "Gm", "Ab", "Bb7", "Cm", "Dm"]; //Eb
		chords[10]= ["Bb", "Cm", "Dm", "Eb", "F7", "Gm", "Am"]; //Bb
		chords[11]= ["F", "Gm", "Am", "Bb", "C7", "Dm", "Em"]; //F
		//we'll be using this to generate chord lists
		//in each key, 0-Major 1-Dorian 2-Phrygian 3-Lydian 4-Mixolydian 5-Minor 6-Locrian
		
		int createHeight = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.Y;
		_display = Image.CreateEmpty(32, createHeight, false, Image.Format.Rgbah);
		DrawKeyboard(0.0f);
	}
	public void UpdateSecondary(float keyUpdateEvent)
	{
		GetNode<Slider>("PianoRoll/KeySecondary").Value = GetNode<Slider>("PianoRoll/KeyPrimary").Value;
	}
	public void DrawKeyboard(float keyUpdateEvent)
	{
		int mode = ((int)GetNode<Slider>("PianoRoll/Mode").Value);
		int modeApply = 0;
		string modeDisplay = "";
		string chordsDisplay = "";
		string relativeKey = "";
		switch (mode)
		{
			case 0: modeDisplay = " Major"; modeApply = 0; break;
			case 1: modeDisplay = " Dorian"; modeApply = -2; break;
			case 2: modeDisplay = " Phrygian"; modeApply = -4; break;
			case 3: modeDisplay = " Lydian"; modeApply = -5; break;
			case 4: modeDisplay = " Mixolydian"; modeApply = -7; break;
			case 5: modeDisplay = " Minor"; modeApply = -9; break;
			case 6: modeDisplay = " Locrian"; modeApply = -11; break;
		}
		int keyPrimary = (((int) GetNode<Slider>("PianoRoll/KeyPrimary").Value)*5)%12;
		int keySecondary = (((int)GetNode<Slider>("PianoRoll/KeySecondary").Value)*5)%12;

		for (int c = 0; c < 7; c++) chordsDisplay += chords[((keyPrimary)*5)%12][(c+mode)%7]+" ";

		int keyOffset = (int)(GetNode<Slider>("PianoRoll/KeyPrimary").Value-GetNode<Slider>("PianoRoll/KeySecondary").Value);
		if (keyOffset > 6) keyOffset = 6;
		if (keyOffset < -6) keyOffset = -6;
		int chordStartKey = (int)GetNode<Slider>("PianoRoll/KeyPrimary").Value;
		if (keyOffset > 0)
		{
			chordsDisplay += "\rRecede - ";
			for (int o = 1; o <= keyOffset; ++o) chordsDisplay += chords[(12+chordStartKey-o)%12][3]+" ";
		}

		if (keyOffset < 0)
		{
			chordsDisplay += "\rAdvance - ";
			for (int o = 1; o <= -keyOffset; ++o) chordsDisplay += chords[(12+chordStartKey+o)%12][6]+" ";
		}
		//this is kinda messy, but it will present the out-of-key chords correctly		
		GetNode<Label>("PianoRoll/Chords").Text = chordsDisplay;
		
		int createWidth = 32;
		int createHeight = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.Y;
		_keysDisplay = Image.CreateEmpty(createWidth, createHeight, false, Image.Format.Rgbah);
		_keysDisplay.Fill(Colors.Black);
		switch (((keyPrimary+12+modeApply)*5)%12)
		{
			case 0: relativeKey = "C"; break;
			case 1: relativeKey = "G"; break;
			case 2: relativeKey = "D"; break;
			case 3: relativeKey = "A"; break;
			case 4: relativeKey = "E"; break;
			case 5: relativeKey = "B"; break;
			case 6: relativeKey = "F#"; break;
			case 7: relativeKey = "Db"; break;
			case 8: relativeKey = "Ab"; break;
			case 9: relativeKey = "Eb"; break;
			case 10: relativeKey = "Bb"; break;
			case 11: relativeKey = "F"; break;
		}
		Color keyMainColor = Colors.Black;
		switch (((keyPrimary)*5)%12) //assigns physical key colors and nothing else
		{
			case 0: keyMainColor = Colors.Tomato; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 1: keyMainColor = Colors.Firebrick; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 2: keyMainColor = Colors.WebPurple; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 3: keyMainColor = Colors.BlueViolet; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 4: keyMainColor = Colors.RoyalBlue; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 5: keyMainColor = Colors.DodgerBlue; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 6: keyMainColor = Colors.MediumAquamarine; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 7: keyMainColor = Colors.DarkCyan; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 8: keyMainColor = Colors.ForestGreen; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 9: keyMainColor = Colors.YellowGreen; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 10: keyMainColor = Colors.Gold; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
			case 11: keyMainColor = Colors.Orange; GetNode<Label>("PianoRoll/KeyRLabel").Text = relativeKey+modeDisplay; break;
		} //assigns color to what the key is

		switch (((keyPrimary)*5)%12) //assigns physical key colors and nothing else
		{
			case 0: GetNode<Label>("PianoRoll/KeyPLabel").Text = "C"; break;
			case 1: GetNode<Label>("PianoRoll/KeyPLabel").Text = "G"; break;
			case 2: GetNode<Label>("PianoRoll/KeyPLabel").Text = "D"; break;
			case 3: GetNode<Label>("PianoRoll/KeyPLabel").Text = "A"; break;
			case 4: GetNode<Label>("PianoRoll/KeyPLabel").Text = "E"; break;
			case 5: GetNode<Label>("PianoRoll/KeyPLabel").Text = "B"; break;
			case 6: GetNode<Label>("PianoRoll/KeyPLabel").Text = "F#"; break;
			case 7: GetNode<Label>("PianoRoll/KeyPLabel").Text = "Db"; break;
			case 8: GetNode<Label>("PianoRoll/KeyPLabel").Text = "Ab"; break;
			case 9: GetNode<Label>("PianoRoll/KeyPLabel").Text = "Eb"; break;
			case 10: GetNode<Label>("PianoRoll/KeyPLabel").Text = "Bb"; break;
			case 11: GetNode<Label>("PianoRoll/KeyPLabel").Text = "F"; break;
		} //assigns color to what the key is
		
		Color keyAlternateColor = Colors.Black;
		switch (((keySecondary+12)*5)%12) //assigns physical key colors and nothing else
		{
			case 0: keyAlternateColor = Colors.Tomato; GetNode<Label>("PianoRoll/KeySLabel").Text = "C"; break;
			case 1: keyAlternateColor = Colors.Firebrick; GetNode<Label>("PianoRoll/KeySLabel").Text = "G"; break;
			case 2: keyAlternateColor = Colors.WebPurple; GetNode<Label>("PianoRoll/KeySLabel").Text = "D"; break;
			case 3: keyAlternateColor = Colors.BlueViolet; GetNode<Label>("PianoRoll/KeySLabel").Text = "A"; break;
			case 4: keyAlternateColor = Colors.RoyalBlue; GetNode<Label>("PianoRoll/KeySLabel").Text = "E"; break;
			case 5: keyAlternateColor = Colors.DodgerBlue; GetNode<Label>("PianoRoll/KeySLabel").Text = "B"; break;
			case 6: keyAlternateColor = Colors.MediumAquamarine; GetNode<Label>("PianoRoll/KeySLabel").Text = "F#"; break;
			case 7: keyAlternateColor = Colors.DarkCyan; GetNode<Label>("PianoRoll/KeySLabel").Text = "Db"; break;
			case 8: keyAlternateColor = Colors.ForestGreen; GetNode<Label>("PianoRoll/KeySLabel").Text = "Ab"; break;
			case 9: keyAlternateColor = Colors.YellowGreen; GetNode<Label>("PianoRoll/KeySLabel").Text = "Eb"; break;
			case 10: keyAlternateColor = Colors.Gold; GetNode<Label>("PianoRoll/KeySLabel").Text = "Bb"; break;
			case 11: keyAlternateColor = Colors.Orange; GetNode<Label>("PianoRoll/KeySLabel").Text = "F"; break;
		} //assigns color to what the key is

		float layerAlpha = 0.618f;
		for (int y = -12; y < createHeight; y++)
		{
			Color physicalKeyColor = Colors.White;
			switch (y%12) //assigns physical key colors and nothing else
			{
				case 0: break;
				case 1: physicalKeyColor = Colors.Black; break;
				case 2: break;
				case 3: physicalKeyColor = Colors.Black; break;
				case 4: break;
				case 5: break;
				case 6: physicalKeyColor = Colors.Black; break;
				case 7: break;
				case 8: physicalKeyColor = Colors.Black; break;
				case 9: break;
				case 10: physicalKeyColor = Colors.Black; break;
				case 11: break;
			} //draws keys in black and white without reference to key signature

			float inv = 1.0f-physicalKeyColor.G;
			Color linePrimaryColor = Color.FromHsv(0.0f, 0.0f, inv, 1.0f);
			Color lineSecondaryColor = Color.FromHsv(0.0f, 0.0f, inv, 1.0f);
			switch ((y+keySecondary) % 12)
			{
				case 0: lineSecondaryColor = keyAlternateColor; break;
				case 1: break;
				case 2: lineSecondaryColor = keyAlternateColor; break;
				case 3: break;
				case 4: lineSecondaryColor = keyAlternateColor; break;
				case 5: lineSecondaryColor = keyAlternateColor; break;
				case 6: break;
				case 7: lineSecondaryColor = keyAlternateColor; break;
				case 8: break;
				case 9: lineSecondaryColor = keyAlternateColor; break;
				case 10: break;
				case 11: lineSecondaryColor = keyAlternateColor; break;
			}

			switch ((y+keyPrimary) % 12)
			{
				case 0: linePrimaryColor = keyMainColor; break;
				case 1: break;
				case 2: linePrimaryColor = keyMainColor; break;
				case 3: break;
				case 4: linePrimaryColor = keyMainColor; break;
				case 5: linePrimaryColor = keyMainColor; break;
				case 6: break;
				case 7: linePrimaryColor = keyMainColor; break;
				case 8: break;
				case 9: linePrimaryColor = keyMainColor; break;
				case 10: break;
				case 11: linePrimaryColor = keyMainColor; break;
			}
			bool usedSecondaryColor = false;
			if (linePrimaryColor.S < 0.1 && lineSecondaryColor.S > 0.1)
			{
				linePrimaryColor = lineSecondaryColor; //if a stripe would not be included, but secondary has it,
				usedSecondaryColor = true; //draw it in secondary color AND prepare to add it to display keyboard too
			} //so you can easily see which notes are added to what's primarily the primary key.
			linePrimaryColor = linePrimaryColor.Lerp(physicalKeyColor, layerAlpha);
			
			if (y >= 0) //begin drawing stripes in specified colors
			{
				_keysDisplay.FillRect(new Rect2I(0,y,createWidth,1),linePrimaryColor);
				if ((y + modeApply + keyPrimary) % 12 == 0) //make a red dot indicating root note
				{
					_keysDisplay.FillRect(new Rect2I(0, y, 1, 1), Color.Color8(255, 0, 0, 255));
				}

				if (y < 12) //if it's the first 12 stripes, update the display keyboard to match
				{
					if (linePrimaryColor.S > 0.1f) linePrimaryColor = physicalKeyColor;
					else linePrimaryColor = Colors.Gray;
					if (usedSecondaryColor) linePrimaryColor = linePrimaryColor.Lerp(keyAlternateColor,0.5f);
					switch (y % 12) //three octaves of keyboard display
					{
						case 0: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyC").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyC").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyC").Color = linePrimaryColor;
							break;
						case 1: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyCD").Color = linePrimaryColor; 
							GetNode<ColorRect>("PianoRoll/Octave2/keyCD").Color = linePrimaryColor; 
							GetNode<ColorRect>("PianoRoll/Octave3/keyCD").Color = linePrimaryColor; 
							break;
						case 2: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyD").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyD").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyD").Color = linePrimaryColor;
							break;
						case 3: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyDE").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyDE").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyDE").Color = linePrimaryColor;
							break;
						case 4: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyE").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyE").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyE").Color = linePrimaryColor;
							break;
						case 5: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyF").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyF").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyF").Color = linePrimaryColor;
							break;
						case 6: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyFG").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyFG").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyFG").Color = linePrimaryColor;
							break;
						case 7: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyG").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyG").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyG").Color = linePrimaryColor;
							break;
						case 8: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyGA").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyGA").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyGA").Color = linePrimaryColor;
							break;
						case 9: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyA").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyA").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyA").Color = linePrimaryColor;
							break;
						case 10: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyAB").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyAB").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyAB").Color = linePrimaryColor;
							break;
						case 11: 
							GetNode<ColorRect>("PianoRoll/Octave1/keyB").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave2/keyB").Color = linePrimaryColor;
							GetNode<ColorRect>("PianoRoll/Octave3/keyB").Color = linePrimaryColor;
							break;
					}
				}
			}
			
			
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is InputEventMouseButton eventMouseButton && eventMouseButton.ButtonIndex == MouseButton.Left && eventMouseButton.Pressed)
		{
			float boundsWidth = GetNode<TextureRect>("PianoRoll").GetRect().Size.X;
			float boundsHeight = GetNode<TextureRect>("PianoRoll").GetRect().Size.Y;
			float clickX = (eventMouseButton.Position.X-GetNode<TextureRect>("PianoRoll").GlobalPosition.X)-0.5f;
			float clickY = (boundsHeight-(eventMouseButton.Position.Y-GetNode<TextureRect>("PianoRoll").GlobalPosition.Y))-0.5f;
			double lowest = (double) GetNode<TextEdit>("PianoRoll/Lowest").Text.ToFloat();
			double highest = (double) GetNode<TextEdit>("PianoRoll/Falsetto").Text.ToFloat()+1;
			double zoomH = 64.0;
			if (highest > lowest+3.0) zoomH = 256.0/(highest-lowest);
			if (clickX < boundsWidth && clickY < boundsHeight && clickX > 0.0 && clickY > 0.0)
			{
				clickX /= 15.8f;
				clickY /= (float)zoomH;
				clickY += (float)lowest;
				if (_display.GetPixel((int) clickX, (int) clickY).A < 0.1 && _keysDisplay.GetPixel((int) clickX, (int) clickY).OkHslS > 0.1)
				{
					Color voiceColor = Color.Color8(0, 0, 0, 255);
					int keyPrimary = (((int) GetNode<Slider>("PianoRoll/KeyPrimary").Value)*5)%12;
					int keySharpsFlats = ((keyPrimary) * 5) % 12; //used later to pick correct sharp or flat signifier
					int belt = GetNode<TextEdit>("PianoRoll/Belt").Text.ToInt()+1;
					if (clickY >= lowest && clickY <= belt) voiceColor = Color.Color8((byte)(255-((belt-clickY)*7)), 0, (byte)(255-((clickY-lowest)*7)), 255);
					if (clickY > belt && clickY <= highest) voiceColor = Color.Color8(0, 255, 0, 255);

					if (voiceColor.OkHslS > 0.1) _display.SetPixel((int) clickX, (int) clickY, voiceColor);
					string noteName = "";
					int noteNumber = (int)(clickY + 3) % 12;
					switch (noteNumber)
					{
						case 0: noteName = "A"; break;
						case 1: 
							noteName = "Bb";
							if (keySharpsFlats > 4 && keySharpsFlats < 7) noteName = "A#";
							break;
						case 2: noteName = "B"; break;
						case 3: noteName = "C"; break;
						case 4:
							noteName = "Db";
							if (keySharpsFlats > 1 && keySharpsFlats < 7) noteName = "C#";
							break;
						case 5: noteName = "D"; break;
						case 6:
							noteName = "Eb";
							if (keySharpsFlats > 3 && keySharpsFlats < 8) noteName = "D#";
							break;
						case 7: noteName = "E"; break;
						case 8: noteName = "F"; break;
						case 9:
							noteName = "Gb";
							if (keySharpsFlats > 0 && keySharpsFlats < 5) noteName = "F#";
							break;
						case 10: noteName = "G"; break;
						case 11:
							noteName = "Ab";
							if (keySharpsFlats > 2 && keySharpsFlats < 7) noteName = "G#";
							break;
					}
					float note = 13.75f * float.Pow(2.0f,(float.Floor(clickY) - 9.0f) / 12.0f);
					if (voiceColor.OkHslS > 0.1) GetNode<Beep>("PianoRoll/Beep").PlayNote(note,(voiceColor.G8 == 255));
					GetNode<Label>("PianoRoll/Note").Text = "#"+((int)(clickY))+" - " + noteName;
					//clickY-9%12 gives you 0 = A to 11 = G#.
				}
				else
				{
					GetNode<Beep>("PianoRoll/Beep").PlayNote(0.0f,true);
					_display.SetPixel((int) clickX, (int) clickY, Color.Color8(0,0,0,0));
				}
			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		double lowest = GetNode<TextEdit>("PianoRoll/Lowest").Text.ToFloat();
		double highest = GetNode<TextEdit>("PianoRoll/Falsetto").Text.ToFloat()+1.0;
		double zoomH = 64.0;
		if (highest > lowest+3.0) zoomH = 256.0/(highest-lowest);
		_cropDisplay = Image.CreateEmpty((int)(_display.GetSize().X), (int)(_display.GetSize().Y/zoomH), false, Image.Format.Rgbah);
		_cropDisplay.BlitRect(_keysDisplay, new Rect2I(0, (int)lowest, (int)(_keysDisplay.GetSize().X), (int)(_keysDisplay.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.BlendRect(_display, new Rect2I(0, (int)lowest, (int)(_display.GetSize().X), (int)(_display.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.FlipY();
		_displayTexture = ImageTexture.CreateFromImage(_cropDisplay);
		GetNode<TextureRect>("PianoRoll").Texture = _displayTexture;
	}
}

