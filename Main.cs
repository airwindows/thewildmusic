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


	public void DrawKeyboard(float keyUpdateEvent)
	{
		
		int keyPrimary = (((int)GetNode<Slider>("PianoRoll/KeyPrimary").Value)*5)%12;
		int keySecondary = (((int)GetNode<Slider>("PianoRoll/KeySecondary").Value)*5)%12;
		int createWidth = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.X;
		int createHeight = (int)GetNode<TextureRect>("PianoRoll").GetRect().Size.Y;
		_keysDisplay = Image.CreateEmpty(createWidth, createHeight, false, Image.Format.Rgbah);
		_keysDisplay.Fill(Colors.Black);
		
		Color keyMainColor = Colors.White;
		string mode = " Major";
		switch (((keyPrimary)*5)%12) //assigns physical key colors and nothing else
		{
			case 0: keyMainColor = Colors.Tomato; GetNode<Label>("PianoRoll/KeyPLabel").Text = "C"+mode; break;
			case 1: keyMainColor = Colors.Firebrick; GetNode<Label>("PianoRoll/KeyPLabel").Text = "G"+mode; break;
			case 2: keyMainColor = Colors.WebPurple; GetNode<Label>("PianoRoll/KeyPLabel").Text = "D"+mode; break;
			case 3: keyMainColor = Colors.BlueViolet; GetNode<Label>("PianoRoll/KeyPLabel").Text = "A"+mode; break;
			case 4: keyMainColor = Colors.RoyalBlue; GetNode<Label>("PianoRoll/KeyPLabel").Text = "E"+mode; break;
			case 5: keyMainColor = Colors.DodgerBlue; GetNode<Label>("PianoRoll/KeyPLabel").Text = "B"+mode; break;
			case 6: keyMainColor = Colors.MediumAquamarine; GetNode<Label>("PianoRoll/KeyPLabel").Text = "F#"+mode; break;
			case 7: keyMainColor = Colors.DarkCyan; GetNode<Label>("PianoRoll/KeyPLabel").Text = "Db"+mode; break;
			case 8: keyMainColor = Colors.ForestGreen; GetNode<Label>("PianoRoll/KeyPLabel").Text = "Ab"+mode; break;
			case 9: keyMainColor = Colors.YellowGreen; GetNode<Label>("PianoRoll/KeyPLabel").Text = "Eb"+mode; break;
			case 10: keyMainColor = Colors.Gold; GetNode<Label>("PianoRoll/KeyPLabel").Text = "Bb"+mode; break;
			case 11: keyMainColor = Colors.Orange; GetNode<Label>("PianoRoll/KeyPLabel").Text = "F"+mode; break;
		} //assigns color to what the key is
		
		Color keyAlternateColor = Colors.White;
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
			Color linePrimaryColor = physicalKeyColor.Lerp(Color.Color8(128,128,128,255), layerAlpha);
			Color lineSecondaryColor = physicalKeyColor.Lerp(Color.Color8(128,128,128,255), layerAlpha);
			
			switch ((y+keySecondary) % 12)
			{
				case 0: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 1: break;
				case 2: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 3: break;
				case 4: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 5: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 6: break;
				case 7: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 8: break;
				case 9: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
				case 10: break;
				case 11: lineSecondaryColor = physicalKeyColor.Lerp(keyAlternateColor, layerAlpha); break;
			}

			switch ((y+keyPrimary) % 12)
			{
				case 0: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 1: break;
				case 2: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 3: break;
				case 4: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 5: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 6: break;
				case 7: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 8: break;
				case 9: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
				case 10: break;
				case 11: linePrimaryColor = physicalKeyColor.Lerp(keyMainColor, layerAlpha); break;
			}
			
			//linePrimaryColor = lineSecondaryColor.Lerp(linePrimaryColor, 0.5f);
			if (linePrimaryColor.S < 0.1 && lineSecondaryColor.S > 0.1) linePrimaryColor = lineSecondaryColor;
			if ((y+keyPrimary) % 12 == 0) linePrimaryColor = keyMainColor.Lerp(Colors.White, layerAlpha);
			if (y >= 0) _keysDisplay.FillRect(new Rect2I(0,y,createWidth,1),linePrimaryColor);
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
			double scrollH = GetNode<Slider>("PianoRoll/ScrollH").Value;
			double scrollW = GetNode<Slider>("PianoRoll/ScrollW").Value;
			if (clickX < boundsWidth && clickY < boundsHeight && clickX > 0.0 && clickY > 0.0)
			{
				clickX /= (float)zoomW;
				clickX += (float)scrollW;
				clickY /= (float)zoomH;
				clickY += (float)scrollH;
				if (_display.GetPixel((int) clickX, (int) clickY).A < 0.1 && _keysDisplay.GetPixel((int) clickX, (int) clickY).OkHslS > 0.1)
				{
					_display.SetPixel((int) clickX, (int) clickY, Color.Color8(255,0,0,255));
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
		double scrollH = GetNode<Slider>("PianoRoll/ScrollH").Value;
		double scrollW = GetNode<Slider>("PianoRoll/ScrollW").Value;
		_cropDisplay = Image.CreateEmpty((int)(_display.GetSize().X/zoomW), (int)(_display.GetSize().Y/zoomH), false, Image.Format.Rgbah);
		_cropDisplay.BlitRect(_keysDisplay, new Rect2I((int)scrollW, (int)scrollH, (int)(_keysDisplay.GetSize().X/zoomW), (int)(_keysDisplay.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.BlendRect(_display, new Rect2I((int)scrollW, (int)scrollH, (int)(_display.GetSize().X/zoomW), (int)(_display.GetSize().Y/zoomH)), new Vector2I(0, 0));
		_cropDisplay.FlipY();
		_displayTexture = ImageTexture.CreateFromImage(_cropDisplay);
		GetNode<TextureRect>("PianoRoll").Texture = _displayTexture;
	}
}