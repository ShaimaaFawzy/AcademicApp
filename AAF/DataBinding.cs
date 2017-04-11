using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAF1
{
   public  class DataBinding
    {
    
   
       public string Text { set; get; }
       public string  Image { get; set; }

       public int ID { set; get; }
       public DataBinding(int _Id,string _Txt,string _Image)

      {
          this.ID = _Id;

         this.  Text = _Txt;
           this.Image = _Image;
       }
    }
    public class DataFill
    {

        public List<DataBinding> Data = new List<DataBinding> ();
        public void Win8Data()
        {
            Data.Add(new DataBinding(0, "مترو القاهرة", "/Assets/TopApps/مترو القاهرة .png"));
            Data.Add(new DataBinding(1, "Science ForKids", "/Assets/TopApps/Science ForKids.png"));
            Data.Add(new DataBinding(2, "Kidzania", "/Assets/TopApps/Icon.png"));
            Data.Add(new DataBinding(3, "Fos7tna", "/Assets/TopApps/Fos7tna.png"));

            Data.Add(new DataBinding(4, "Otere Shooter", "/Assets/TopApps/Otere Shooter.png"));
          

            
        }

        public void WpData()
        {


            Data.Add(new DataBinding(0, "Egypt Taxi Fare", "/Assets/TopApps/e76f321b-afa5-4da4-ab83-34751af80f84.png"));
            Data.Add(new DataBinding(1, "Islamy", "/Assets/TopApps/a43adafb-3397-4089-bae2-2521ca88da3c.png"));
            Data.Add(new DataBinding(2, "Sha2ety", "/Assets/TopApps/f37b0c46-89c3-4381-a797-998e3b593235.png"));
            Data.Add(new DataBinding(3, "Coffee Time", "/Assets/TopApps/39f41005-fb92-4462-b2c6-f00aa14b1940.png"));

            Data.Add(new DataBinding(4, "The Cooker", "/Assets/TopApps/d716d4a4-f133-426b-858d-q.png"));
          

        }
       

    }
}


