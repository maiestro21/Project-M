using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;
using MetroFramework.Forms;
using System.Globalization;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Text.RegularExpressions;
using System.Web;
using NAudio;
using NAudio.Wave;



namespace Project_M
{
    public partial class Form1 : MetroForm
    {

        private SpeechRecognitionEngine listener;
        private SpeechSynthesizer speaker;

        private NotifyIcon trayIcon;

        private bool leaveMeAlone = false;
        private bool voice = true;


        public Form1()
        {
            InitializeComponent();
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            speaker = new SpeechSynthesizer();
            speaker.SelectVoice("Microsoft Zira Desktop");
            
            printMessage("Hello darling");
            


            GrammarBuilder builder = new GrammarBuilder();
            builder.Append(new Choices((new string[] { "close","leave me alone","go away","fuck off","exit","stop speaking","stop talking","be quiet","silence","hello","i'm fine","i'm good","good","fine","thank you","thanks", "what's the time", "what time is it","can you tell me the time","can you tell me what time it is"
                ,"time","what's the date","what day is it","what is today's date","what is today","today","you may speak now",
            "you may speak","speak","sound on","come back","facebook","date","google","youtube","search","opera","my computer","computer",
            "open my computer","files", "open files","open my files", "what the fuck", "switch to voice", "switch to text",
            "activate voice", "yes","play some music","play music","play my music","play my playlist","music", "voice activated", "voice deactivated",
            "who are you", "what is your name", "can you hear me", "do you hear me", "party", "friends", "tell me a joke", "joke", "funny", "are you listening",
            "you can see me now"})));
            //builder.AppendDictation();

            Grammar grammar = new Grammar(builder);

            listener = new SpeechRecognitionEngine();
            listener.LoadGrammar(grammar);
            listener.SetInputToDefaultAudioDevice();
            listener.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(listener_SpeechRecognized);
            listener.RecognizeAsync(RecognizeMode.Multiple);
            wishHello();
            
        }

        void listener_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string commandName = e.Result.Text;
          //  MessageBox.Show(commandName);

            if(leaveMeAlone==true)
            switch(commandName.ToLower())
            {
                case "you may speak now":
                case "you may speak":
                case "speak":
                case "sound on":
                case "come back":
                    if(voice==true)
                    {
                        speaker.Speak("Thank you");
                    }
                    else
                    {
                        printMessage("Thank you");
                    }
                    
                    
                    label1.Text = "I'm listening";
                    leaveMeAlone = false;
                    this.Show();
                    break;
            }
            if (leaveMeAlone == false)
            {
                switch (commandName.ToLower())
                {
                    case "close":
                    case "leave me alone":
                    case "go away":
                    case "fuck off":
                    case "exit":


                        if (voice == true)
                        {
                            speaker.Speak("Talk to you later");
                        }
                        else
                        {
                            printMessage("Talk to you later");
                        }
                        this.Hide();

                        leaveMeAlone = true;
                        label1.Text = "I am not listening";
                        break;





                    case "hello":

                        if (voice == true)
                        {
                            speaker.Speak("Hello. How are you doing?");
                        }
                        else
                        {
                            printMessage("Hello. How are you doing?");
                        }
                        label1.Text = "☺";
                        label1.Text = "Hello";
                        break;

                    case "yes":

                        if (voice == true)
                        {
                            speaker.Speak("What can I do for you?");
                        }
                        else
                        {
                            printMessage("What can I do for you?");
                        }
                        label1.Text = "?";
                        break;

                    case "i'm fine":
                    case "i'm good":
                    case "good":
                    case "fine":
                        label1.Text = ":D";
                        if (voice == true)
                        {
                            speaker.Speak("I'm glad to hear that");
                        }
                        else
                        {
                            printMessage("I'm glad to hear that");
                        }
                        break;

                    case "thank you":
                        label1.Text = ":)";
                        if (voice == true)
                        {
                            speaker.Speak("You're welcome");
                        }
                        else
                        {
                            printMessage("You're welcome");
                        }
                        label1.Text = "You are welcome";
                        break;

                    case "thanks":
                        label1.Text = ";)";
                        if (voice == true)
                        {
                            speaker.Speak("No problemo");
                        }
                        else
                        {
                            printMessage("No problemo");
                        }
                        break;

                    case "what's the time":
                    case "what time is it":
                    case "can you tell me the time":
                    case "can you tell me what time it is":
                    case "time":

                        speaker.SpeakAsync(DateTime.Now.ToShortTimeString());
                        

                        break;

                    case "what's the date":
                    case "what day is it":
                    case "what is today's date":
                    case "what is today":
                    case "today":
                    case "date":

                        speaker.SpeakAsync(DateTime.Today.ToString("dddd, MMMM d, yyyy"));

                        break;

                    case "facebook":


                        Process.Start("http://www.facebook.com");
                        break;

                    case "google":
                    case "search":
                    case "opera":
                        label1.Text = "Google time";
                        Process.Start("http://www.google.com");
                        break;

                    case "youtube":

                        Process.Start("http://www.youtube.com");
                        break;

                    case "my computer":
                    case "computer":
                    case "open my computer":
                    case "files":
                    case "open files":
                    case "open my files":
                        string myComputerPath = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                        Process.Start("explorer", myComputerPath);
                        break;

                    case "what the fuck":
                        if (voice == true)
                        {
                            speaker.Speak("Do you need help, Gabriel?");
                        }
                        else
                        {
                            printMessage("Do you need help, Gabriel?");
                        }
                        label1.Text = "WTF?";
                        break;

                    case "activate voice":
                    case "voice activated":
                    case "switch to voice":
                       
                         speaker.Speak("Voice activated");
                        label1.Text="Voice on nigga";
                        
                        voice = true;
                        break;

                        
                  
                    case "stop speaking":
                    case "stop talking":
                    case "voice deactivated":
                    case "be quiet":
                    case "silence":
                    case "switch to text":
                        voice = false;
                        printMessage("I'm mute");
                        label1.Text = "Ok I'm muted";
                        break;



                    case "who are you":
                    case "what is your name":
                        label1.Text = "M";
                        if (voice == true)
                        {
                            speaker.Speak("My name is M. Your personal assistant");
                        }
                        else
                        {
                            printMessage("My name is M. Your personal assistant");
                        }
                        break;

                    case "can you hear me":
                    case "do you hear me":
                        label1.Text = "YES";
                        if (voice == true)
                        {
                            speaker.Speak("Yes");
                        }
                        else
                        {
                            printMessage("Yes");
                        }
                        break;


                    case "play some music":
                    case "play music":
                    case "play my music":
                    case "music":
                    case "play my playlist":

                        Process.Start("www.youtube.com/playlist?list=FLblHYro_OYFvXGrGuzRycCw&playnext_from=PL&index=0&playnext=1");
                        if (voice == true)
                        {
                            speaker.Speak("Let the music ring ring ring");
                        }
                        else
                        {
                            printMessage("PAAAARTYYY HOUSE");
                        }
                        label1.Text = "Music all over the houseeee";
                        
                        break;


                    case "party":
                    case "friends":
                    

                        //Process.Start("start C:\\Users\\puiu_\\AppData\\Local\\Discord\\Update.exe --processStart Discord.exe");
                        if (voice == true)
                        {
                            speaker.Speak("Discord party");
                        }
                        else
                        {
                            printMessage("Discord party");
                        }
                        label1.Text = "Discord partyy";
                        
                        break;



                    
                    case "are you listening":


                        //Process.Start("start C:\\Users\\puiu_\\AppData\\Local\\Discord\\Update.exe --processStart Discord.exe");
                        if (voice == true)
                        {
                            speaker.Speak("Yes");
                        }
                        else
                        {
                            printMessage("Yes");
                        }
                        label1.Text = "Ye";

                        break;


                    case "you can see me now":
                        label1.Text = "Rapanon";

                        //Process.Start("start C:\\Users\\puiu_\\AppData\\Local\\Discord\\Update.exe --processStart Discord.exe");
                        if (voice == true)
                        {
                            speaker.Speak("Rapanon");
                        }
                        else
                        {
                            printMessage("Rapanon");
                        }
                        
                                 IWavePlayer waveOutDevice = new WaveOut();
                                 AudioFileReader audioFileReader = new AudioFileReader("j.mp3");
                                 waveOutDevice.Init(audioFileReader);
                                 waveOutDevice.Play();
                        break;


                    case "joke":
                    case "funny":
                    case "tell me a joke":

                        string result="Offline";
                        using (var webClient = new System.Net.WebClient())
                        {
                            result = webClient.DownloadString("https://api.apithis.net/yomama.php");
                        }

                        //Process.Start("start C:\\Users\\puiu_\\AppData\\Local\\Discord\\Update.exe --processStart Discord.exe");
                        if (voice == true)
                        {
                            speaker.Speak(result);
                        }
                        else
                        {
                            printMessage(result);
                        }
                        label1.Text = result;

                        break;

                        

                    default:

                        //handle non-normalized recognition

                        //speaker.SpeakAsync("Can you repeat honey?");

                        //example, probably should URL encode the value...
                        //Process.Start("http://www.google.com?q=" + commandName);


                        break;
                }
                
            }
        }

       

      

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //be sure to clean up!
            listener.UnloadAllGrammars();
            listener.Dispose();
            listener = null;

            speaker.Dispose();
            speaker = null;

           
        }

        
        void wishHello()
        {
            speaker.Speak("Hello darling");
            
        }

        void printMessage(string message)
        {
            notifyIcon1.BalloonTipTitle = "M";
            notifyIcon1.BalloonTipText = message;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
     

       

       

        
    }
}
