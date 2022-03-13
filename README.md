  # WAV_Creator

  # Usage

  ```
  WAV_Creator.WAV wav = new WAV_Creator.WAV();
  wav.Create(440, 50, 10, folderPath);
  ```

  OR
  
  ```
  WAV_Creator.WAV wav = new WAV_Creator.WAV();

  int amplitude = 40;
  int sampleRate = 44100;
  int duration = 3;
  int frequency = 440;

  for (int i = 0; i < duration * sampleRate; i++)
  {

      double theta = frequency * ((double)i / 44100) * (2 * Math.PI);

      short sampleValue = (short)(amplitude * (Math.Sin(theta)));

      wav.AddSample(sampleValue);
  }

  wav.Export($@"D:\Sample_WAV.wav");
  ```
  
  
