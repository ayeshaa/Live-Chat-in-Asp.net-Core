using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ConflictRenewal.Data;
using ConflictRenewal.Models;
using ConflictRenewal.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ConflictRenewal.Pages.Chat
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IEnumerable<IdentityUser> Users { get; set; }

        public bool IsAdminConflict { get; set; }

        public RoleEnum rollEnum { get; set; }

        // public ConversationViewModel Conversationsview { get; set; }

        public Conversations Conversationsview { get; set; }
        public List<UserConversationVM> ConversationAddedUsers { get; set; }
        // public IList<UserConversationVM> userConversationsview { get; set; }
        public UserConversationVM userConversationsview { get; set; }


        LatestConversationVM conversationVM = new LatestConversationVM();

        public void OnGetAsync(string id) //public void OnGet(string id) //
        {
            //id = "0d800ac4-1f77-4e79-9277-e646c4d283ef";
            //string userId = User.Identity.Name;
            string UserName = User.Identity.Name;
            var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
            var userId = CurrentUser.Id;
            if (userId == null)
            {
                // return RedirectToAction("Index", "Home", null);
            }
            if (id != null)
            {
                if (userId == id)
                {
                    id = null;
                }
            }
            var UserId = User.Identity.Name;

            var UserPIC = _context.Users.Where(x => x.Id == UserId).FirstOrDefault();

            //if (UserPIC.ProfilePic != null && UserPIC.ProfilePic.Length > 1)
            //{

            //    TempData["ActiveUserpic"] = UserPIC.ProfilePic;
            //}
            //else
            //{
            ViewData["ActiveUserpic"] = "Profileplaceholder.png";
            // }
            ViewData["UserId"] = userId;

            if (!string.IsNullOrEmpty(id))
            {

                var U = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                var conversations = _context.Conversations.Where(x => x.From == id || x.To == id).FirstOrDefault();
                if (conversations == null)
                {
                    Conversations dcon = new Conversations();
                    dcon.To = id;
                    dcon.From = userId;
                    dcon.IsSeen = true;
                    dcon.Date = DateTime.Now;
                    dcon.Text = "Now You Can Start Chat With Me";

                    _context.Conversations.Add(dcon);
                    _context.SaveChanges();
                }

                ViewData["ActiveUserId"] = id;

                ViewData["ActiveUserName"] = U.UserName;
                //if (U.ProfilePic != null)
                //{
                //    if (U.ProfilePic.Length < 1)
                //    {
                //    ViewData["ActiveUserpic"] = U.ProfilePic;
                //    }
                //}
                //if (U.ProfilePic == null)
                //{
                ViewData["ActiveUserpic"] = "Profileplaceholder.png";
                //}

                Conversationsview = conversations;
               
            }

            ConversationAddedUsers = _AddedUsers();
        }
        public IActionResult OnGetSendMessage(string id)
        {
            //this.Users = _userManager.Users.Include(u => u.role).ToList();
            //var user = await _userManager.FindByEmailAsync("bill@gmail");
            //var roles = await _userManager.GetRolesAsync(user);

            var Coach = _context.Users.Where(x => x.Email == "bill@gmail.com").FirstOrDefault();
            if(id==null)
            id = Coach.Id;

            // var userRoles = await UserManager.GetRolesAsync(User);
            string UserName = User.Identity.Name;
            var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
            var userId = CurrentUser.Id;
            if (userId == null)
            {
                return new JsonResult(null);
            }
            if (id != null)
            {
                if (userId == id)
                {
                    id = null;
                }
            }
            var UserId = CurrentUser.Id;
            var UserPIC = _context.Users.Where(x => x.Id == UserId).FirstOrDefault();

            //if (UserPIC.ProfilePic != null && UserPIC.ProfilePic.Length > 1)
            //{

            //    TempData["ActiveUserpic"] = UserPIC.ProfilePic;
            //}
            //else
            //{
            //    TempData["ActiveUserpic"] = "Profileplaceholder.png";
            //}
            List<Conversations> con = null;
            List<LatestConversationVM> messages = new List<LatestConversationVM>();
            if (!string.IsNullOrEmpty(id))
            {
                var U = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                con = _context.Conversations.Where(x => x.From == UserId || x.To == UserId).ToList();
                if (con == null)
                {
                    Conversations dcon = new Conversations();
                    dcon.To = id;
                    dcon.From = userId;
                    dcon.IsSeen = true;
                    dcon.Date = DateTime.Now;
                    dcon.Text = "Now You Can Start Chat With Me";

                    _context.Conversations.Add(dcon);
                    _context.SaveChanges();
                }
                else
                {
                    foreach (var item in con)
                    {
                        LatestConversationVM NM = new LatestConversationVM();
                        var user = _context.Users.Find(item.From);
                        var fromUser = _context.Users.Where(x => x.UserName == item.From).FirstOrDefault();
                        var fnameEmail = user.Email;
                        var fname = fnameEmail.Split('@');
                        NM.From = item.From;
                        //NM.FromName = user.FirstName + " " + user.LastName;
                        NM.FromName = fname[0];
                        NM.Date = item.Date;
                        if (item.Text == "")
                        {
                            NM.Text = "New Image";
                        }
                        else
                        {
                            NM.Text = item.Text;
                        }

                        NM.Image = "Profileplaceholder.png";// user.ProfilePic;
                        
                        if (NM.Image == null || NM.Image.Length < 1)
                        {
                            NM.Image = "Profileplaceholder.png";
                        }
                        messages.Add(NM);


                    }
                }

                //if (U.ProfilePic != null)
                //{
                //    if (U.ProfilePic.Length < 1)
                //    {
                //        ViewBag.ActiveUserpic = U.ProfilePic;
                //    }
                //}
                //if (U.ProfilePic == null)
                //{
                //    ViewBag.ActiveUserpic = "Profileplaceholder.png";
                //}

                //var UserId = User.Identity.GetUserId();
                //var UserPIC = db.AspNetUsers.Where(x => x.Id == UserId).FirstOrDefault();
                //if (UserPIC.ProfilePic == null || UserPIC.ProfilePic.Length<1)
                //{
                //    Session["UserDP"] = "Profileplaceholder.png";
                //}
                //else
                //{
                //    Session["UserDP"] = UserPIC.ProfilePic;
                //}
                ViewData["ActiveUserId"] = id;
                ViewData["ActiveUserName"] = U.Email;

            }
            ViewData["UserId"] = userId;
            ViewData["CurrentUserName"] = User.Identity.Name;
            //return JsonResult(
            //new { status = "success", data = conversations },
            //JsonRequestBehavior.AllowGet);
            return new JsonResult(messages);
        }

        public IActionResult OnPostSendMessage(string text, string To)
        {
            string message = "";
            string From = User.Identity.Name;
            List<ConversationVM> result = new List<ConversationVM>();
            string UserName = User.Identity.Name;
            var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
            var userId = CurrentUser.Id;
            Conversations obj = new Conversations();

            try
            {
                //HttpPostedFileBase file = (HttpPostedFileBase)null;

                //HttpFileCollectionBase files = Request.Files;
                string fname = string.Empty;
                var Message = text;
                MemoryStream stream = new MemoryStream();
                Request.Body.CopyTo(stream);
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream))
                {
                    string requestBody = reader.ReadToEnd();
                    if (requestBody.Length > 0)
                    {
                        var rdata = JsonConvert.DeserializeObject<ConversationPostData>(requestBody);
                        if (rdata != null)
                        {
                            Message = rdata.text;
                            To = rdata.To;
                        }
                    }
                }
                //if (files.Count > 0)
                //{
                //    file = files[0];

                //    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                //    {
                //        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                //        fname = testfiles[testfiles.Length - 1];
                //    }
                //    else
                //    {
                //        fname = file.FileName;
                //    }
                //}

                //var chkMessage = Message.ToLower();
                //if (chkMessage.Contains("@") || chkMessage.Contains("gmail") || chkMessage.Contains("yahoo") || chkMessage.Contains("live") ||
                //    chkMessage.Contains("hotmail") || chkMessage.Contains("facebook") || chkMessage.Contains("whatsapp") || chkMessage.Contains("address") || chkMessage.Contains("email")
                //    || chkMessage.Contains("street") || chkMessage.Contains("house") || chkMessage.Contains("#") || chkMessage.Contains("no") || chkMessage.Contains("city") || chkMessage.Contains("country")
                //    || chkMessage.Contains("contry") || chkMessage.Contains("skype") || chkMessage.Contains("imo") || chkMessage.Contains("twitter") || chkMessage.Contains("pay") || chkMessage.Contains("instagram")
                //    || chkMessage.Contains("payment") || chkMessage.Contains("card") || chkMessage.Contains("cash") || chkMessage.Contains("location") || chkMessage.Contains("social") || chkMessage.Contains("personal")
                //    || chkMessage.Contains("contact") || chkMessage.Contains("phone") || chkMessage.Contains("mobile"))
                //{
                //    Response.StatusCode = (int)System.Net.HttpStatusCode.Forbidden;
                //    message = "Error";
                //    return Json(new { data = message }, JsonRequestBehavior.AllowGet);
                //}

                // var To = Request.Form[1];
                // var FileType = Request.Form[2];



                if (!string.IsNullOrWhiteSpace(To))
                {
                    obj.Text = Message;
                    obj.To = To;
                }


                obj.Date = DateTime.Now;
                obj.From = userId;
                int count = 0;

                if (!string.IsNullOrWhiteSpace(fname))
                {
                    //if (FileType == "Photo")
                    //{
                    //    obj.Image = fname;
                    //}

                    count++;
                }
                string status;
                try
                {
                    _context.Conversations.Add(obj);
                    _context.SaveChanges();
                    status = "Ok";
                }
                catch (Exception)
                {
                    status = "Error";
                    throw;
                }

                //if (count == 1 && status == "Ok")
                //{
                //    fname = System.IO.Path.Combine(Server.MapPath("~/Uploads/ChatImages"), fname);
                //    file.SaveAs(fname);
                //}
                if (status == "Ok")
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                    message = "Ok";
                }
                else
                {
                    Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                    message = "Error";
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                message = "Error";
            }


            var UserId = User.Identity.Name;
            var results = _context.Conversations.Where(x => x.From == From && x.To == UserId || x.From == UserId && x.To == From && x.IsSeen == false).ToList();

            var query = from x in _context.Conversations
                        where x.From == From && x.To == UserId || x.From == UserId && x.To == From && x.IsSeen == false
                        select new ConversationVM
                        {
                            From = x.From,
                            IsSeen = x.IsSeen,
                            Text = x.Text,
                            Date = x.Date,
                            Image = x.Image,
                        };

            foreach (var item in query)
            {
                var sender = _context.Users.Where(x => x.Id == From).FirstOrDefault();
                if (item.FromImage == null || item.FromImage.Length < 1)
                {
                    item.FromImage = "Profileplaceholder.png";
                }
                else
                {
                    item.FromImage = "Profileplaceholder.png"; //sender.ProfilePic;
                }
                result.Add(item);

            }
            results.ToList();
            var data = _context.Conversations.Where(x => x.Mid == obj.Mid).FirstOrDefault();
            data.Image = "Profileplaceholder.png";
            return new JsonResult(data);

        }

        public IActionResult OnGetGetLastestChat()
        {
            try
            {
                string UserName = User.Identity.Name;
                var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
                var userId = CurrentUser.Id;

                var query = from r in _context.Conversations
                                //  where (r.To == userId && r.Text != null) || r.From == userId)
                                //select r).ToList().Take(5);
                            where r.To == userId && r.Text != null || r.From == userId
                            orderby r.Date descending
                            group r by r.From into g
                            select g.OrderByDescending(x => x.Date).Take(5).FirstOrDefault();

                List<LatestConversationVM> messages = new List<LatestConversationVM>();

                foreach (var item in query)
                {
                    LatestConversationVM NM = new LatestConversationVM();
                    var user = _context.Users.Find(item.From);
                    var fromUser = _context.Users.Where(x => x.UserName == item.From).FirstOrDefault();
                    var fnameEmail = user.Email;
                    var fname = fnameEmail.Split('@');
                    NM.From = item.From;
                    //NM.FromName = user.FirstName + " " + user.LastName;
                    NM.FromName = fname[0];
                    NM.Date = item.Date;
                    if (item.Text == "")
                    {
                        NM.Text = "New Image";
                    }
                    else
                    {
                        NM.Text = item.Text;
                    }

                    NM.Image = "";// user.ProfilePic;
                    if (NM.Image == null || NM.Image.Length < 1)
                    {
                        NM.Image = "Profileplaceholder.png";
                    }
                    messages.Add(NM);


                }

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                //return Json(messages, JsonRequestBehavior.AllowGet);
                return new JsonResult(messages);

            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                // return Json(new { data = "Error" }, JsonRequestBehavior.AllowGet);
                return new JsonResult("Error");

            }

        }

        public List<UserConversationVM> _AddedUsers()
        {
            List<Conversations> addedUsr = new List<Conversations>();
            List<AspNetUser> refinedUsr = new List<AspNetUser>();
            List<UserConversationVM> query = new List<UserConversationVM>();

            string UserName = User.Identity.Name;
            var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
            var userId = CurrentUser.Id;



            var results = _context.Conversations.Where(x => x.From == userId || x.To == userId).ToList();
            foreach (var item in results)
            {
                bool alreadyExists = addedUsr.Any(x => x.From == item.From && x.To == item.To);
                if (addedUsr.Count() == 0)
                {
                    addedUsr.Add(item);
                }
                else if (alreadyExists == false)
                {
                    bool alreadyExists2 = addedUsr.Any(x => x.To == item.From && x.From == item.To);
                    if (alreadyExists2 == false)
                    {
                        addedUsr.Add(item);
                    }
                }

            }
            addedUsr.ToList();


            foreach (var item in addedUsr)
            {
                var usr = _context.Users.Where(x => x.Id != userId && (x.Id == item.From || x.Id == item.To)).FirstOrDefault();
                var Concount = _context.Conversations.Where(a => a.To == userId && a.From == usr.Id && a.IsSeen == false);
                int count = 0;
                if (Concount != null)
                {
                    count = Concount.Count();
                }
                UserConversationVM con = new UserConversationVM();
                con.Id = usr.Id;
                con.ImageUrl = "";// usr.ProfilePic;
                                  //if (usr.IsLogin == null)
                                  //{
                                  //    con.IsLogin = true;
                                  //}
                                  //else
                                  //{
                                  //    con.IsLogin = (bool)usr.IsLogin;
                                  //}
                con.UserName = usr.UserName; // usr.FirstName + " " + usr.LastName;
                con.count = count;
                query.Add(con);
            }


            //var query = from c in db.AspNetUsers
            //            where c.Id != userId && c.UserName != "admin"
            //            select new UserConversationVM
            //            {
            //                Id = c.Id,
            //                UserName = c.UserName,
            //                IsLogin = true,
            //                ImageUrl = c.ProfilePic,
            //                count = c.Conversations.Where(a => a.To == userId && a.IsSeen == false).Count()

            //            };

            //var query = from c in db.AspNetUsers
            //            where c.Id != null
            //            select new UserConversationVM
            //            {
            //                Id = c.Id,
            //                UserName = c.UserName,
            //                IsLogin = true,
            //                ImageUrl = c.ProfilePic,
            //                count = c.Conversations.Count()

            //            };
            var newQuery = query.ToList().OrderByDescending(x => x.Id);
            //  ConversationAddedUsers = newQuery.ToList();
            return newQuery.ToList();

        }
        public ActionResult OnGetGetConversation(string From)
        {
            try
            {
                List<ConversationVM> result = new List<ConversationVM>();

                string UserName = User.Identity.Name;
                var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
                var UserId = CurrentUser.Id;
                var UserPIC = _context.Users.Where(x => x.Id == UserId).FirstOrDefault();
                //Session["UserDP"] = UserPIC.ProfilePic;

                //if (UserPIC.ProfilePic != null || UserPIC.ProfilePic.Length < 1)
                //{
                //    ViewBag.ActiveUserpic = UserPIC.ProfilePic;
                //}
                //else
                //{
                //    ViewBag.ActiveUserpic = "Profileplaceholder.png";
                //}

                var results = _context.Conversations.Where(x => x.From == From && x.To == UserId && x.IsSeen == false).ToList();

                foreach (var x in results)
                {
                    x.IsSeen = true;
                }
                _context.SaveChanges();

                var query = from x in _context.Conversations
                            from u in _context.Users.Where(y => y.Id == x.From)
                            where x.From == UserId && x.To == From || x.From == From && x.To == UserId
                            select new ConversationVM
                            {
                                From = x.From,
                                IsSeen = x.IsSeen,
                                Text = x.Text,
                                Date = x.Date,
                                Image = x.Image,
                                //   FromImage = u.ProfilePic,

                            };

                foreach (var item in query)
                {
                    var sender = _context.Users.Where(x => x.Id == item.From).FirstOrDefault();
                    //Session["UserDP"] = sender.ProfilePic;
                    item.FromImage = "Profileplaceholder.png"; // sender.ProfilePic;

                    if (item.FromImage == null || item.FromImage.Length < 1)
                    {
                        item.FromImage = "Profileplaceholder.png";
                    }

                    result.Add(item);

                }
                //result = query.ToList();

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return new JsonResult(result);

            }
            catch (Exception e)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult("Error in conversation");
            }

        }
        public ActionResult OnGetGetUnreadConversation(string From)
        {
            try
            {
                List<ConversationVM> result = new List<ConversationVM>();
                string UserName = User.Identity.Name;
                var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
                var UserId = CurrentUser.Id;

               
                  //  _context.Configuration.ProxyCreationEnabled = false;

                    var results = _context.Conversations.Where(x => x.From == From && x.To == UserId && x.IsSeen == false).ToList();



                    var query = from x in _context.Conversations
                                where x.From == From && x.To == UserId && x.IsSeen == false
                                select new ConversationVM
                                {
                                    From = x.From,
                                    IsSeen = x.IsSeen,
                                    Text = x.Text,
                                    Date = x.Date,
                                    Image = x.Image

                                };

                    foreach (var item in query)
                    {
                        var sender = _context.Users.Where(x => x.Id == From).FirstOrDefault();
                        //if (sender.ProfilePic == null || sender.ProfilePic.Length < 1)
                        //{
                        //    item.FromImage = "Profileplaceholder.png";
                        //}
                        //else
                        //{
                            item.FromImage = "Profileplaceholder.png"; // sender.ProfilePic;
                       // }
                        result.Add(item);

                    }


                    results.ToList();
                    foreach (var x in results)
                    {
                        x.IsSeen = true;
                    }
                    _context.SaveChanges();

                Response.StatusCode = (int)System.Net.HttpStatusCode.OK;
                return new JsonResult(result);
            }
            catch (Exception exe)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                return new JsonResult("Error in conversation");

            }
        }

        public ActionResult OnGetNotification()
        {
            string UserName = User.Identity.Name;
            var CurrentUser = _context.Users.Where(x => x.Email == UserName).FirstOrDefault();
            var userId = CurrentUser.Id;


            var query = _context.Conversations.Where(x => x.To == userId && x.IsSeen == false).Count();
                int notif = query;
            var results = string.Empty;
            if (notif == 0)
            {
                results = null;
            }
            else if (notif > 0)
            {
                 results = "<small id='Notification' style='bottom: 15px; left: 12px; border-radius: 50 %; padding: 5px 10px; background: green; color: white;float:right; margin-right: 30px; border-radius: 20px; '>" + notif + "</small>";

            }
           
            return Content(results);
        }

    }
}