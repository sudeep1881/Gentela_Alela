using gentela_Alela.Models;
using System.Collections.Generic;

namespace gentela_Alela.Views.View_Model
{
   

    public class VoiceListViewModel
    {
        public List<Voice> Voices { get; set; } = new();

        // Total count for easy access in view
        public int TotalVoices => Voices?.Count ?? 0;  // Optional, success/error messages
    }

}
