using Microsoft.AspNetCore.Mvc;
using ScSoMeAPI.Models;

namespace ScSoMeAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TimelineController
    {
        [HttpGet]
        [Route("GetAllComments")]
        public List<Comment> GetAllComments()
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Comment> comments = new List<Comment>();
            try
            {
                foreach (var comment in ctx.Comment)
                {
                        comments.Add(new Comment
                        {
                            commentID = comment.commentID,
                            username = comment.username,
                            comment = comment.comment,
                            likes = comment.likes,
                            postID = comment.postID,
                            createdDate = comment.createdDate,
                        });
                }

            }
            catch (Exception e)
            {
                comments = new List<Comment>{ new Comment
                { username = "ERROR",
                  comment = e.Message,
                  likes = 0,
                  postID= 0,
                  createdDate = DateTime.Now,
                } 
                };

            }
            return comments;
        }

        [HttpPost]
        [Route("AddComment")]
        public Comment PostComment([FromBody] Comment com)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {
                
                var result = ctx.Comment.Add(com);
                ctx.SaveChanges();

            }
            catch (Exception e)
            {
                com = new Comment
                {
                    username = "Error",
                    comment = e.Message,
                    likes = 0,
                    postID = 0,
                    createdDate= DateTime.Now,

                };

            }
            return com;
        }

        [HttpPost]
        [Route("AddPost")]
        public Post PostPost([FromBody] Post post)
        {
            using BachelordbContext ctx = new BachelordbContext();

            try
            {

                var result = ctx.Post.Add(post);
                ctx.SaveChanges();

            }
            catch (Exception e)
            {
                post = new Post
                {
                    username = "Error",
                    content = e.Message,
                    likes = 0,
                    createdDate = DateTime.Now,
                };

            }
            return post;
        }

        [HttpGet]
        [Route("GetAllPosts")]
        public List<Post> GetAllPosts(string username)
        {
            using BachelordbContext ctx = new BachelordbContext();
            List<Post> posts = new List<Post>();
            List<string> connections = new List<string>();
            try
            {
                foreach(var connection in ctx.Connections.Where(c => c.UsernameCon1 == username || c.UsernameCon2 == username))
                {
                    if(connection.UsernameCon1 != username)
                    {
                        connections.Add(connection.UsernameCon1);
                    }
                    else if(connection.UsernameCon2 != username)
                    {
                        connections.Add(connection.UsernameCon2);
                    }
                }
                foreach (var post in ctx.Post.Where(p => p.username == username))
                {
                    posts.Add(new Post
                    {
                        postID = post.postID,
                        username = post.username,
                        content = post.content,
                        likes = post.likes,
                        createdDate = post.createdDate,
                    });
                }
                foreach (var con in connections)
                {
                    foreach (var post in ctx.Post.Where(p => p.username.Equals(con)))
                    {
                        posts.Add(new Post
                        {
                            postID = post.postID,
                            username = post.username,
                            content = post.content,
                            likes = post.likes,
                            createdDate = post.createdDate,
                        });
                    }
                }

            }
            catch (Exception e)
            {
                posts = new List<Post>{ new Post
                { username = "ERROR",
                  content = e.Message,
                  likes = 0,
                  createdDate =(DateTime)DateTime.Now,
                }
                };

            }
            return  posts.OrderByDescending(p => p.createdDate).ToList();
         
        }

        [HttpPost]
        [Route("LikePost")]
        public void LikePost([FromBody] string isLikedAndPostID)
        {
            using BachelordbContext ctx = new BachelordbContext();
            string[] split = isLikedAndPostID.Split(", ");
            int postID = int.Parse(split[1]);
            bool isLiked = Convert.ToBoolean(split[0]);
            var postToUpdate = ctx.Post.Where(p => p.postID == postID).ToList();
            foreach (var post in postToUpdate)
            {
                if(isLiked)
                {
                    post.likes++;
                }
                else
                {
                    post.likes--;
                }
                
            }
            ctx.SaveChanges();
        }

        [HttpPost]
        [Route("LikeComment")]
        public void LikeComment([FromBody] string isLikedAndCommentID)
        {
            using BachelordbContext ctx = new BachelordbContext();
            string[] split = isLikedAndCommentID.Split(", ");
            int commentID = int.Parse(split[1]);
            bool isLiked = Convert.ToBoolean(split[0]);
            var commentToUpdate = ctx.Comment.Where(c => c.commentID == commentID).ToList();
            foreach (var comment in commentToUpdate)
            {
                if (isLiked)
                {
                    comment.likes++;
                }
                else
                {
                    comment.likes--;
                }

            }
            ctx.SaveChanges();
        }
    }
}
