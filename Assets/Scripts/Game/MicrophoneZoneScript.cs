using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class MicrophoneZoneScript : MonoBehaviour
{
    #region Public Attributs
    private const int SAMPLECOUNT = 1024;   // Sample Count.
    private const float REFVALUE = 0.1f;    // RMS value for 0 dB.
    private const float THRESHOLD = 0.02f;  // Minimum amplitude to extract pitch (recieve anything)
    public int clamp = 160;                 // Used to clamp dB (I don't really understand this either).
    public bool isBlowing;
    #endregion

    #region Private Attributs
    private AudioSource _AudioSource;
    private List<GameObject> ListObjectsActif = new List<GameObject>();

    private float rmsValue;     // Volume in RMS
    private float dbValue;      // Volume in DB
    private float[] samples = new float[SAMPLECOUNT];    // Samples
    private float[] spectrum = new float[SAMPLECOUNT];   // Spectrum
    #endregion

    // Use this for initialization
    void Start ()
    {
        _AudioSource = gameObject.GetComponent<AudioSource>();
        LaunchCapture();
    }

    // Update is called once per frame
    void Update ()
    {
        if (!_AudioSource.isPlaying)
            LaunchCapture();
        AnalyzeSound();
        if (dbValue > 0.0f)
        {
            isBlowing = true;
            foreach (GameObject go in ListObjectsActif)
            {
                Destroy(go);
            }
        }
        else
            isBlowing = false;
    }

    void LaunchCapture()
    {
        Assert.IsFalse(Microphone.devices.Length < 0);
        _AudioSource.clip = Microphone.Start(Microphone.devices[0], true, 1, AudioSettings.outputSampleRate);
        while (Microphone.GetPosition(Microphone.devices[0]) <= 0) {}
        _AudioSource.Play();
    }

    void OnTriggerEnter(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_VAPOR:
                ListObjectsActif.Add(parCollider.gameObject);
                break;
        }
    }

    void OnTriggerExit(Collider parCollider)
    {
        switch (parCollider.gameObject.layer)
        {
            case Const.LAYER_VAPOR:
                ListObjectsActif.Remove(parCollider.gameObject);
                break;
        }
    }
    
    private void AnalyzeSound()
    {
        // Get all of our samples from the mic.
        _AudioSource.GetOutputData(samples, 0);

        // Sums squared samples
        float sum = 0;
        for (int i = 0; i < SAMPLECOUNT; i++)
            sum += Mathf.Pow(samples[i], 2);

        // RMS is the square root of the average value of the samples.
        rmsValue = Mathf.Sqrt(sum / SAMPLECOUNT);
        dbValue = 20 * Mathf.Log10(rmsValue / REFVALUE);

        // Clamp it to {clamp} min
        if (dbValue < -clamp)
            dbValue = -clamp;

        // Gets the sound spectrum.
        _AudioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        int maxN = 0;

        // Find the highest sample.
        for (int i = 0; i < SAMPLECOUNT; i++)
        {
            if (spectrum[i] > maxV && spectrum[i] > THRESHOLD)
            {
                maxV = spectrum[i];
                maxN = i; // maxN is the index of max
            }
        }

        // Pass the index to a float variable
        float freqN = maxN;
 
        // Interpolate index using neighbours
        if (maxN > 0 && maxN < SAMPLECOUNT - 1)
        {
            float dL = spectrum[maxN - 1] / spectrum[maxN];
            float dR = spectrum[maxN + 1] / spectrum[maxN];
            freqN += 0.5f * (dR* dR - dL* dL);
        }
    }
}
