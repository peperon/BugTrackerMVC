[1mdiff --git a/BugTracker/BugTracker/Controllers/ErrorsController.cs b/BugTracker/BugTracker/Controllers/ErrorsController.cs[m
[1mindex b6f818b..e3f3463 100644[m
[1m--- a/BugTracker/BugTracker/Controllers/ErrorsController.cs[m
[1m+++ b/BugTracker/BugTracker/Controllers/ErrorsController.cs[m
[36m@@ -40,6 +40,7 @@[m [mnamespace BugTracker.Controllers[m
                          where error.ProjectId == projectId[m
                          select new ErrorModel[m
                          {[m
[32m+[m[32m                             Id = error.ErrorId,[m
                              DateCreation = error.DateCreation,[m
                              Priority = error.Priority.ToString(),[m
                              UserName = error.User.UserName,[m
[36m@@ -64,27 +65,11 @@[m [mnamespace BugTracker.Controllers[m
             return View();[m
         }[m
 [m
[31m-        private void FillViewBag(int id)[m
[31m-        {[m
[31m-            ViewBag.Priorities = new List<SelectListItem>[m
[31m-            {[m
[31m-                new SelectListItem { Text = "Low", Value = ((int)ErrorPriority.Low).ToString() },[m
[31m-                new SelectListItem { Text = "Normal", Value = ((int)ErrorPriority.Normal).ToString() },[m
[31m-                new SelectListItem { Text = "Hight", Value = ((int)ErrorPriority.Hight).ToString() },[m
[31m-                new SelectListItem { Text = "Critical", Value = ((int)ErrorPriority.Critical).ToString() },[m
[31m-            };[m
[31m-[m
[31m-            ViewBag.Projects = _projectRepo.Projects.ToList()[m
[31m-                .Select(project =>[m
[31m-                    new SelectListItem { Text = project.ProjectName, Value = project.ProjectId.ToString(), Selected = project.ProjectId == id }[m
[31m-                    );[m
[31m-[m
[31m-        }[m
[31m-[m
         //[m
         // POST: /Errors/Create[m
 [m
         [HttpPost][m
[32m+[m[32m        [ValidateAntiForgeryToken][m
         public ActionResult Create(Error error)[m
         {[m
             try[m
[36m@@ -165,5 +150,22 @@[m [mnamespace BugTracker.Controllers[m
                 return View();[m
             }[m
         }[m
[32m+[m
[32m+[m[32m        private void FillViewBag(int id)[m
[32m+[m[32m        {[m
[32m+[m[32m            ViewBag.Priorities = new List<SelectListItem>[m
[32m+[m[32m            {[m
[32m+[m[32m                new SelectListItem { Text = "Low", Value = ((int)ErrorPriority.Low).ToString() },[m
[32m+[m[32m                new SelectListItem { Text = "Normal", Value = ((int)ErrorPriority.Normal).ToString() },[m
[32m+[m[32m                new SelectListItem { Text = "Hight", Value = ((int)ErrorPriority.Hight).ToString() },[m
[32m+[m[32m                new SelectListItem { Text = "Critical", Value = ((int)ErrorPriority.Critical).ToString() },[m
[32m+[m[32m            };[m
[32m+[m
[32m+[m[32m            ViewBag.Projects = _projectRepo.Projects.ToList()[m
[32m+[m[32m                .Select(project =>[m
[32m+[m[32m                    new SelectListItem { Text = project.ProjectName, Value = project.ProjectId.ToString(), Selected = project.ProjectId == id }[m
[32m+[m[32m                    );[m
[32m+[m
[32m+[m[32m        }[m
     }[m
 }[m
[1mdiff --git a/BugTracker/BugTracker/Models/ErrorModel.cs b/BugTracker/BugTracker/Models/ErrorModel.cs[m
[1mindex bfba9dc..b8fe475 100644[m
[1m--- a/BugTracker/BugTracker/Models/ErrorModel.cs[m
[1m+++ b/BugTracker/BugTracker/Models/ErrorModel.cs[m
[36m@@ -7,6 +7,8 @@[m [mnamespace BugTracker.Models[m
 {[m
     public class ErrorModel[m
     {[m
[32m+[m[32m        public int Id { get; set; }[m
[32m+[m
         public string UserName { get; set; }[m
 [m
         public string Priority { get; set; }[m
[1mdiff --git a/BugTracker/BugTracker/Views/Errors/Index.cshtml b/BugTracker/BugTracker/Views/Errors/Index.cshtml[m
[1mindex e96c53c..91dcbc2 100644[m
[1m--- a/BugTracker/BugTracker/Views/Errors/Index.cshtml[m
[1m+++ b/BugTracker/BugTracker/Views/Errors/Index.cshtml[m
[36m@@ -14,24 +14,33 @@[m
         .BindTo(Model)[m
         .Render();[m
     }[m
[31m-    @Html.ActionLink("Create New", "Create", null, new { id = "create-link" })[m
[32m+[m[32m    @*@Html.ActionLink("Create New", "Create", null, new { id = "create-link" })*@[m
 </p>[m
 <div>[m
     @{[m
         Html.Kendo().Grid<BugTracker.Models.ErrorModel>()[m
             .Name("grid")[m
[32m+[m[32m            .Resizable(res => res.Columns(true))[m
[32m+[m[32m            .ToolBar(toolbar =>[m[41m [m
[32m+[m[32m                {[m
[32m+[m[32m                    toolbar.Custom().Action("Create", "Errors", null).HtmlAttributes(new { id = "create-link" }).Text("Create new error");[m
[32m+[m[32m                })[m
             .Columns(column =>[m
                 {[m
                     column.Bound(error => error.UserName).Title("Owner");[m
                     column.Bound(error => error.Priority).Title("Priority");[m
                     column.Bound(error => error.DateCreation).Title("Date found");[m
[32m+[m[32m                    column.Command(command => command.Edit());[m
[32m+[m[32m                    //column.Command(command => command.Destroy());[m
                 }[m
             )[m
             .Sortable()[m
             .Pageable()[m
             .DataSource(datasource => datasource[m
                 .Ajax()[m
[32m+[m[32m                .Model(model => model.Id("Id"))[m
                 .Read(read => read.Action("Errors", "Errors").Data("drop_down_value"))[m
[32m+[m[32m                .Update(edit => edit.Action("Edit", "Errors"))[m
             ).Render();        [m
     }[m
 </div>[m
[36m@@ -48,10 +57,9 @@[m
         return { projectId: value };[m
     }[m
 [m
[31m-    function refresh_grid() {[m
[31m-        alert("test");[m
[32m+[m[32m    function refresh_grid() {[m[41m        [m
         var grid = $("#grid").data("kendoGrid");[m
[31m-        grid.read();[m
[32m+[m[32m        grid.dataSource.read();[m
         grid.refresh();[m
     }[m
 </script>[m
