<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HTTPDriver.TestSite._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Test Site</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>
            Test Page</h1>
        <div id="page-body">
            Hello world</div>
        <ul>
            <li class="nav-item">Item 1</li><li class="nav-item">Item 2</li></ul>
        <a id="linkToAnotherPage" href="AnotherPage.aspx">Click here to follow a link.</a>
    </div>
    </form>
</body>
</html>
