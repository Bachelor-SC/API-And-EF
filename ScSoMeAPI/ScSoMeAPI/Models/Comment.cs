namespace ScSoMeAPI.Models
{
    public partial class Comment
    {
        public int commentID { get; set; }
        public string username { get; set; }
        public string comment { get; set; }
        public int likes { get; set; }
        public int postID { get; set; }

        public DateTime? createdDate { get; set; }
    }
}
