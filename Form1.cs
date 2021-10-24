﻿using System;
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

        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            speechSynthesizerObj.Dispose();
            if (richTextBox1.Text != "")
            {
                speechSynthesizerObj = new SpeechSynthesizer();
                speechSynthesizerObj.SpeakAsync(richTextBox1.Text);
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                return;
            }

            SaveFileDialog savefile = new SaveFileDialog();
            // set a default file name
            savefile.FileName = "unknown.mp3";
            // set filters - this can be done in properties as well
            savefile.Filter = "Mp3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                //using (StreamWriter sw = new StreamWriter(savefile.FileName))
                  //  sw.WriteLine("Hello World!");


                using (SpeechSynthesizer synth = new SpeechSynthesizer())
                {
                    synth.SetOutputToWaveFile(savefile.FileName);
                    PromptBuilder builder = new PromptBuilder();
                    builder.AppendText(richTextBox1.Text);
                    synth.Speak(builder);

                    MessageBox.Show("Saved Succesfully");
                }
            }

            //using (SpeechSynthesizer synth = new SpeechSynthesizer())
            //{
            //    synth.SetOutputToWaveFile(@"G:\Speech\SpeechTo22.mp3");
            //    PromptBuilder builder = new PromptBuilder();
            //    builder.AppendText(richTextBox1.Text);
            //    synth.Speak(builder);


            //}


        }
    }
}
