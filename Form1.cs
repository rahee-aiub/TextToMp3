using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Text2Speech
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speechSynthesizerObj;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            speechSynthesizerObj = new SpeechSynthesizer();
            btnSpeak.Enabled = true;
            btnResume.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
           
            speechSynthesizerObj.Dispose();
            if (richTextBox1.Text != "")
            {               
                speechSynthesizerObj = new SpeechSynthesizer();
                speechSynthesizerObj.SpeakAsync(richTextBox1.Text);

              
                btnResume.Enabled = true;
                btnPause.Enabled = true;
                btnStop.Enabled = true;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                return;
            }

            SaveFileDialog savefile = new SaveFileDialog();          
            savefile.FileName = "unknown.mp3";           
            savefile.Filter = "Mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            if (savefile.ShowDialog() == DialogResult.OK)
            {             
                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {                   
                    synth.SetOutputToWaveFile(savefile.FileName);
                    PromptBuilder builder = new PromptBuilder();
                    builder.AppendText(richTextBox1.Text);
                    synth.Speak(builder);

                    MessageBox.Show("Saved Succesfully");
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            btnSpeak.Enabled = true;
            btnResume.Enabled = false;
            btnPause.Enabled = false;
            btnStop.Enabled = false;
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
               
                if (speechSynthesizerObj.State == SynthesizerState.Speaking)
                {                      
                    speechSynthesizerObj.Pause();
                    btnResume.Enabled = true;
                    btnSpeak.Enabled = false;
                }
            }
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                if (speechSynthesizerObj.State == SynthesizerState.Paused)
                {
                    speechSynthesizerObj.Resume();
                    btnResume.Enabled = false;
                    btnSpeak.Enabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (speechSynthesizerObj != null)
            {
                speechSynthesizerObj.Dispose();
                btnSpeak.Enabled = true;
                btnResume.Enabled = false;
                btnPause.Enabled = false;
                btnStop.Enabled = false;
            }
        }
    }
}
