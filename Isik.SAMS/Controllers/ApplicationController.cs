﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Isik.SAMS.Models.Entity;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Isik.SAMS.Controllers
{

    public class ApplicationController : Controller
    {
        StudentApprovalManagementEntities db = new StudentApprovalManagementEntities();
        // GET: Application
        public ActionResult Index()
        {
            var user = db.SAMS_Users.Find(Convert.ToInt32(Session["UserId"]));
            var model = db.SAMS_StudentApplications.ToList();
            //Alt kısım mesaj verme olayı çözülünce uncomment yapılacak. 
            //if (user.UserType == 1) 
            //{
            //    model = db.SAMS_StudentApplications.Where(x => x.Status != 2).ToList();
            //} else 
            //{
            //    model = db.SAMS_StudentApplications.Where(x => x.Status != 1).ToList();
            //}
            
            foreach (var a in model)
            {
                var dep = db.SAMS_Department.Find(a.DepartmentId);
                if (dep != null)
                {
                    a.DepartmentName = dep.DepartmentName;
                }

                var prog = db.SAMS_Program.Find(a.ProgramId);
                if (prog != null)
                {
                    a.ProgramName = prog.ProgramName;
                }

                var statusName = db.SAMS_ApplicationStatus.Find(a.Status);
                if (statusName != null)
                {
                    a.StatusName = statusName.StatusName;
                }
            }

            ViewBag.Message = TempData["message"] == null ? null : TempData["message"].ToString();
            ViewBag.MessageClass = TempData["messageClass"] == null ? null : TempData["messageClass"].ToString();
            return View(model);
        }
        public ActionResult Detail(int? id)
        {
            var model = db.SAMS_StudentApplications.ToList();

            foreach (var a in model)
            {
                var dep = db.SAMS_Department.Find(a.DepartmentId);
                if (dep != null)
                {
                    a.DepartmentName = dep.DepartmentName;
                }

                var prog = db.SAMS_Program.Find(a.ProgramId);
                if (prog != null)
                {
                    a.ProgramName = prog.ProgramName;
                }

                var statusName = db.SAMS_ApplicationStatus.Find(a.Status);
                if (statusName != null)
                {
                    a.StatusName = statusName.StatusName;
                }

            }
            if (id != null)
            {
                var application = db.SAMS_StudentApplications.Find(id);
                var files = db.SAMS_Files.Where(x => x.StudentApplicationId == id).ToList();
                if (files != null)
                {
                    ViewBag.IsEducationalInfoEntered = "true";
                    foreach (var a in files)
                    {
                        if (a.FileName.Contains("HighSchoolTranscript"))
                        {
                            application.highSchoolTranscriptContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("ResidencePermit"))
                        {
                            application.residencePermitContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("EquivalenceCertificate"))
                        {
                            application.equivalenceCertificateContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("HighSchoolDiploma"))
                        {
                            application.highSchoolDiplomaContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("StudentPhoto"))
                        {
                            application.studentPhotoContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("InternationalExamScore"))
                        {
                            application.internationalExamScoreContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("CopyofPassportorIDCard"))
                        {
                            application.IdorPassportCopyContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("EnglishLanguageProficiencyScore"))
                        {
                            application.englishLanguageProfScoreContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("CV"))
                        {
                            application.cvContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("BachelorDiploma"))
                        {
                            application.bachelorDiplomaContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("BachelorTranscript"))
                        {
                            application.bachelorTranscriptContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("MasterDiploma"))
                        {
                            application.masterDiplomaContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("MasterTranscript"))
                        {
                            application.masterTranscriptContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("ReferenceLetter1"))
                        {
                            application.referenceLetter1ContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }
                        else if (a.FileName.Contains("ReferenceLetter2"))
                        {
                            application.referenceLetter2ContentResult = File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                        }

                    }
                }
                return View(application);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public FileResult DownloadFile(int id, string name)
        {
            var files = db.SAMS_Files.Where(x => x.StudentApplicationId == id).ToList();
            if (files != null)
            {
                foreach (var a in files)
                {
                    if (a.FileName == name + "" + a.FileExtension)
                    {
                        return File(a.FileData, MimeMapping.GetMimeMapping(a.FileName), a.FileName);
                    }
                }
            }
            return null;
        }

        public ActionResult ApprovalMessage()
        {
            if (TempData["isDeleted"] != null)
            {
                TempData["Message"] = "Operation failed.";
                TempData["messageClass"] = "alert-danger";
            }
            else
            {
                TempData["Message"] = "Succesfully approved.";
                TempData["messageClass"] = "alert-success";
            }
            TempData.Keep("Message");
            TempData.Keep("messageClass");
            return RedirectToAction("Index");
        }

        public JsonResult Approval(int? id)
        {
            bool result = false;
            var applications = db.SAMS_StudentApplications.Find(id);
            var dep = db.SAMS_Department.Find(applications.DepartmentId);
            if (dep != null)
            {
                applications.DepartmentName = dep.DepartmentName;
            }

            var prog = db.SAMS_Program.Find(applications.ProgramId);
            if (prog != null)
            {
                applications.ProgramName = prog.ProgramName;
            }

            if (applications != null)
            {
                var user = db.SAMS_Users.Find(Convert.ToInt32(Session["UserId"]));
                if(user.UserType == 1)
                {
                    applications.Status = 2;
                    applications.ApprovedBy = Convert.ToInt32(Session["UserId"]);
                    db.SaveChanges();
                    var email = new MimeMessage();
                    var from = "SAMS SAMS";
                    var subject = "SAMS info - Application Status Update";
                    email.From.Add(new MailboxAddress(from, "samsinfo.noreply@gmail.com"));
                    email.To.Add(new MailboxAddress(applications.Email, applications.Email));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = @"<h1> As the SAMS team, </h1>" +
                        @"<h3>We are happy to say that your application has been passed the first phase of the enrollment process.</h3>" +
                        @"<br/>" +
                        @"<p>Your application has been approved by the secretary of the Department: " + applications.DepartmentName.ToString() + " and Program: " + applications.ProgramName.ToString() + " you have selected.</p>" +
                        @"<br/>" +
                        @"<p>The application is on the evaluation process of the Head of the " + applications.DepartmentName + " department.</p>" +
                        @"<br/>" +
                         @"<p>Please stay tuned.</p>" +
                        @"<br/>"
                    };

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("samsinfo.noreply@gmail.com", "qultbqdkozwvfhgt");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        TempData["IsMailSent"] = "true";
                    }
                } 
                else
                {
                    applications.Status = 4;
                    applications.ApprovedBy = Convert.ToInt32(Session["UserId"]);
                    db.SaveChanges();
                    var email = new MimeMessage();
                    var from = "SAMS SAMS";
                    var subject = "SAMS info - Application Status Update";
                    email.From.Add(new MailboxAddress(from, "samsinfo.noreply@gmail.com"));
                    email.To.Add(new MailboxAddress(applications.Email, applications.Email));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = @"<h1> As the SAMS team, </h1>" +
                        @"<h3>We are happy to say that your application has been passed the second phase of the enrollment process.</h3>" +
                        @"<br/>" +
                        @"<p>Your application has been approved by the Head of the Department: " + applications.DepartmentName.ToString() + " you have selected.</p>" +
                        @"<br/>" +
                        @"<p>You will be enrolled in a later day.</p>" +
                        @"<br/>" +
                         @"<p>Please stay tuned.</p>" +
                        @"<br/>"
                    };

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("samsinfo.noreply@gmail.com", "qultbqdkozwvfhgt");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        TempData["IsMailSent"] = "true";
                    }
                }
                result = true;
            }
            else
            {
                TempData["isApprovedBySecretary"] = false;
                TempData["isApprovedByHoD"] = false;
                TempData.Keep("isApprovedBySecretary");
                TempData.Keep("isApprovedByHoD");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RejectMessage()
        {
            if (TempData["isDeleted"] != null)
            {
                TempData["Message"] = "Operation failed.";
                TempData["messageClass"] = "alert-danger";
            }
            else
            {
                TempData["Message"] = "Succesfully Rejected.";
                TempData["messageClass"] = "alert-success";
            }
            TempData.Keep("Message");
            TempData.Keep("messageClass");
            return RedirectToAction("Index");
        }

        public JsonResult Reject(int? id)
        {
            bool result = false;
            var applications = db.SAMS_StudentApplications.Find(id);
            if (applications != null)
            {
                var user = db.SAMS_Users.Find(Convert.ToInt32(Session["UserId"]));
                if (user.UserType == 1)
                {
                    applications.Status = 5;
                    applications.RejectedBy = Convert.ToInt32(Session["UserId"]);
                    db.SaveChanges();
                    var email = new MimeMessage();
                    var from = "SAMS SAMS";
                    var subject = "SAMS info - E-Mail Verification";
                    email.From.Add(new MailboxAddress(from, "samsinfo.noreply@gmail.com"));
                    email.To.Add(new MailboxAddress(applications.Email, applications.Email));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = @"<h1> As the SAMS team, </h1>" +
                        @"<h3>We are really sorry to tell you that your application has been rejected.</h3>" +
                        @"<br/>" +
                        @"<p></p>" +
                        @"<br/>"
                    };

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("samsinfo.noreply@gmail.com", "qultbqdkozwvfhgt");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        TempData["IsMailSent"] = "true";
                    }
                }
                else
                {
                    applications.Status = 5;
                    applications.RejectedBy = Convert.ToInt32(Session["UserId"]);
                    db.SaveChanges();
                    var email = new MimeMessage();
                    var from = "SAMS SAMS";
                    var subject = "SAMS info - E-Mail Verification";
                    email.From.Add(new MailboxAddress(from, "samsinfo.noreply@gmail.com"));
                    email.To.Add(new MailboxAddress(applications.Email, applications.Email));
                    email.Subject = subject;
                    email.Body = new TextPart(TextFormat.Html)
                    {
                        Text = @"<h1> As the SAMS team, </h1>" +
                        @"<h3>We are really sorry to tell you that your application has been rejected.</h3>" +
                        @"<br/>" +
                        @"<p>Even tho you have been passed the previous phase. Your application wasn't met the standards of the second phase.</p>" +
                        @"<br/>"
                    };

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Connect("smtp.gmail.com", 465, true);
                        smtp.Authenticate("samsinfo.noreply@gmail.com", "qultbqdkozwvfhgt");
                        smtp.Send(email);
                        smtp.Disconnect(true);
                        TempData["IsMailSent"] = "true";
                    }
                }
                result = true;
            }
            else
            {
                TempData["isApprovedBySecretary"] = false;
                TempData["isApprovedByHoD"] = false;
                TempData.Keep("isApprovedBySecretary");
                TempData.Keep("isApprovedByHoD");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}