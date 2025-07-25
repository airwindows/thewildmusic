using Godot;

namespace TheWildMusic;
public partial class Main : Node2D
{
	private Image _display;
	private Image _keysDisplay;
	private Image _cropDisplay;
	private ImageTexture _displayTexture;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		int createWidth = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.X;
		int createHeight = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.Y;
		_display = Image.CreateEmpty(createWidth, createHeight, false, Image.Format.Rgbah);
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
		int createWidth = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.X;
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
		switch (((keySecondary)*5)%12) //assigns physical key colors and nothing else
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

		float layerAlpha = 0.618f;//(float)GetNode<Slider>("PianoRoll/Alpha").Value;
		
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
			
			if (linePrimaryColor.S < 0.1 && lineSecondaryColor.S > 0.1) linePrimaryColor = lineSecondaryColor;
			linePrimaryColor = linePrimaryColor.Lerp(physicalKeyColor, layerAlpha);
			
			if (y >= 0)
			{
				_keysDisplay.FillRect(new Rect2I(0,y,createWidth,1),linePrimaryColor);
				if ((y+modeApply+keyPrimary) % 12 == 0) linePrimaryColor = Color.Color8(255,0,0,255);
				_keysDisplay.FillRect(new Rect2I(0, y, 5, 1), linePrimaryColor);
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
			double zoomH = GetNode<Slider>("PianoRoll/ZoomH").Value;
			double zoomW = GetNode<Slider>("PianoRoll/ZoomW").Value;
			double scrollH = GetNode<ScrollBar>("PianoRoll/ScrollH").Value;
			double scrollW = GetNode<ScrollBar>("PianoRoll/ScrollW").Value;
			if (clickX < boundsWidth && clickY < boundsHeight && clickX > 0.0 && clickY > 0.0)
			{
				clickX /= (float)zoomW;
				clickX += (float)scrollW;
				clickY /= (float)zoomH;
				clickY += (float)scrollH;
				if (_display.GetPixel((int) clickX, (int) clickY).A < 0.1 && _keysDisplay.GetPixel((int) clickX, (int) clickY).OkHslS > 0.1)
				{
					Color voiceColor = Color.Color8(0, 0, 0, 255);
					int lowest = GetNode<TextEdit>("PianoRoll/Lowest").Text.ToInt();
					int belt = GetNode<TextEdit>("PianoRoll/Belt").Text.ToInt()+1;
					int falsetto = GetNode<TextEdit>("PianoRoll/Falsetto").Text.ToInt()+1;
					if (clickY >= lowest && clickY <= belt) voiceColor = Color.Color8((byte)(255-((belt-clickY)*7)), 0, (byte)(255-((clickY-lowest)*7)), 255);
					if (clickY > belt && clickY <= falsetto) voiceColor = Color.Color8(0, 255, 0, 255);

					if (voiceColor.OkHslS > 0.1) _display.SetPixel((int) clickX, (int) clickY, voiceColor);
					string noteName = "";
					int noteNumber = (int)(clickY + 3) % 12;
					switch (noteNumber)
					{
						case 0: noteName = "A"; break;
						case 1: noteName = "Bb"; break;
						case 2: noteName = "B"; break;
						case 3: noteName = "C"; break;
						case 4: noteName = "C#"; break;
						case 5: noteName = "D"; break;
						case 6: noteName = "D#"; break;
						case 7: noteName = "E"; break;
						case 8: noteName = "F"; break;
						case 9: noteName = "F#"; break;
						case 10: noteName = "G"; break;
						case 11: noteName = "G#"; break;
					}
					float note = 13.75f * float.Pow(2.0f,(float.Floor(clickY) - 9.0f) / 12.0f);
					if (voiceColor.OkHslS > 0.1) GetNode<Beep>("PianoRoll/Beep").PlayNote(note,(voiceColor.G8 == 255));
					GetNode<Label>("PianoRoll/Note").Text = ((int)(clickY)).ToString()+" "+((int)(clickY+3)%12).ToString() + " " +noteName;
					//clickY-9%12 gives you 0 = A to 11 = G#.
				}
				else
				{
					_display.SetPixel((int) clickX, (int) clickY, Color.Color8(0,0,0,0));
				}
			}
		}

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		double zoomH = GetNode<Slider>("PianoRoll/ZoomH").Value;
		double zoomW = GetNode<Slider>("PianoRoll/ZoomW").Value;
		double scrollH = GetNode<ScrollBar>("PianoRoll/ScrollH").Value;
		double scrollW = GetNode<ScrollBar>("PianoRoll/ScrollW").Value;
		_cropDisplay = Image.CreateEmpty((int)(_display.GetSize().X/zoomW), (int)(_display.GetSize().Y/zoomH), false, Image.Format.Rgbah);
		_cropDisplay.BlitRect(_keysDisplay, new Rect2I((int)scrollW, (int)scrollH, (int)(_keysDisplay.GetSize().X/zoomW), (int)(_keysDisplay.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.BlendRect(_display, new Rect2I((int)scrollW, (int)scrollH, (int)(_display.GetSize().X/zoomW), (int)(_display.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.FlipY();
		_displayTexture = ImageTexture.CreateFromImage(_cropDisplay);
		GetNode<TextureRect>("PianoRoll").Texture = _displayTexture;
	}
}