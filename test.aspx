<%@ Page Language="c#" %>

<%@ Import Namespace="System.IO" %>


<%@ Import Namespace="System.Xml" %>
<%@ Import Namespace="System.Net" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Text" %>
<script language="c#" runat="server">        

    public void Page_Load(object sender, EventArgs e)
    {

        StringBuilder result = new StringBuilder();
        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
	


        WebRequest request = null;
        request = WebRequest.Create(Request.QueryString["url"]);
        request.Method = "GET";       
                
        using (WebResponse response = request.GetResponse())
        {
            using (Stream responseStream = response.GetResponseStream())
            {
                using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                {
                    result.Append(readStream.ReadToEnd());
                    readStream.Close();
                }
                responseStream.Close();
            }
            response.Close();
        }



        Response.Write(result.ToString());


    }
</script>
