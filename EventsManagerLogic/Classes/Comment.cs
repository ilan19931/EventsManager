using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Classes
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Title { get; set; }
        public string UsernamePosted { get; set; }
        public string Details { get; set; }
        public string DateCreated { get; set; }

        public Comment()
        {

        }

        public Comment(int i_Id, int i_UserId, string i_Username, int i_EventId, string i_CommentDetails, string i_DateCreated)
        {
            this.Id = i_Id;
            this.UserId = i_UserId;
            this.EventId = i_EventId;
            this.UsernamePosted = i_Username;
            this.DateCreated = i_DateCreated;
            this.Title = $"{DateCreated} posted by: {UsernamePosted}";
            this.Details = i_CommentDetails;
        }

        public Comment(DataRow data)
        {
            if(data != null)
            {
                this.Id = (int)data["id"];
                this.UserId = (int)data["UserId"];
                this.EventId = (int)data["EventId"];
                this.UsernamePosted = data["usernamePosted"].ToString();
                this.DateCreated = data["DateCreated"].ToString();
                this.Title = $"{DateCreated} posted by: {UsernamePosted}";
                this.Details = data["Details"].ToString();
            }
        }
    }
}
