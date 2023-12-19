using System;

namespace Visual_Art_Galary.entity
{

    public class Artwork
    {
        public int ArtworkID { get; set; }       //Primary Key
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string medium {  get; set; }
        public string ImageURL { get; set; }
        public int artistID { get; set; }

        public Artwork()
        { 
        }
        public Artwork( int artrworkid, string title,string disc,DateTime Cdate, string med, string IURl, int artistid)
        {
            ArtworkID = artrworkid;
            Title = title;
            Description = disc;
            CreationDate = Cdate;
            medium = med;
            ImageURL = IURl;
            artistID=artistid;
        }

        public override string ToString()
        {
            string formattedDate = CreationDate.ToString("MM/dd/yyyy"); 

            return $"{ArtworkID,-8} : {Title,-18} : {Description,-45} : {formattedDate,-15} : {medium,-20} : {ImageURL,-50} : {artistID,-8}";
        }


    }
}
