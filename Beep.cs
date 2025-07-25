using Godot;
using System;

public partial class Beep : AudioStreamPlayer
{
	[Export] public AudioStreamPlayer Player { get; set; }
	private AudioStreamGeneratorPlayback _playback;
	private double phase = 0.0;
	
	public void PlayNote(float pulseHz, bool falsetto)
	{ 
		if (Player.Stream is AudioStreamGenerator generator) { 
			Player.Play(); 
			_playback = (AudioStreamGeneratorPlayback)Player.GetStreamPlayback(); 
			float increment = pulseHz / generator.MixRate; 
			int framesAvailable = _playback.GetFramesAvailable(); 
			for (int i = 0; i < framesAvailable; i++)
			{
				float outputSample = (float) Mathf.Sin(phase * Mathf.Tau);
				outputSample *= (1.0f-((float)i/(float)framesAvailable));
				if (falsetto == false) outputSample = Mathf.Sin(outputSample*2.0f);
				outputSample *= (1.0f-((float)i/(float)framesAvailable));
				if (i < (20000.0f/pulseHz)) outputSample *= (i * (1.0f/(20000.0f/pulseHz)));
				_playback.PushFrame(new Vector2(outputSample*0.8f,outputSample*0.8f)); 
				phase = Mathf.PosMod(phase + increment, 1.0);
			}
		}
	}
}
