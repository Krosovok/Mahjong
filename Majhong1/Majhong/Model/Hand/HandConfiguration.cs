using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahjong.Model.HandSpace
{
    public class HandConfiguration
    {
        public List<HandForm> Forms { get; set; }

        public HandConfiguration(List<HandForm> forms)
        {
            this.Forms = forms;
        }

        public HandConfiguration()
        {
            this.Forms = new List<HandForm>();
        }


        public override bool Equals(object obj)
        {
            if(obj == null || obj.GetType() != this.GetType()
                || this.Forms.Count != (obj as HandConfiguration).Forms.Count) { return false; }

            List<HandForm> first =  new List<HandForm>(this.Forms);
            List<HandForm> second = new List<HandForm>((obj as HandConfiguration).Forms);

            foreach(HandForm form in first)
            {
                second.Remove(form);
            }

            return second.Count == 0;
             
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
