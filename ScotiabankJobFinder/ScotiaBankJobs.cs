using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net;
using System.Xml;
using System.Diagnostics;
/*
*This program utilizes the Html Agility Pack in order to parse html files easily.
*The program takes the root node and finds all nodes within the tableSeachResult class with the tag <a> and an id with search_result.
*It then stores its name and url in a seperate object list
*/

namespace ScotiabankJobFinderProgram
{
    public class ScotiaBankJobs
    {
        static void Main(string[] args)
        {
            var html = new HtmlDocument();
            try//loads html document from the url
            {
                html.LoadHtml(new WebClient().DownloadString("http://jobs.scotiabank.com/search/advanced-search/ASCategory/-1/ASPostedDate/-1/ASCountry/Canada/ASState/-1/ASCity/-1/ASLocation/-1/ASCompanyName/-1/ASCustom1/-1/ASCustom2/-1/ASCustom3/-1/ASCustom4/-1/ASCustom5/-1/ASIsRadius/false/ASCityStateZipcode/-1/ASDistance/-1/ASLatitude/-1/ASLongitude/-1/ASDistanceType/-1"));
            }
            catch (Exception)
            {
                Debug.WriteLine("Error loading html");
            }

            List<Job> Joblist = new List<Job>();//creates new list of jobs
            var root = html.DocumentNode;
            var table = root.Descendants().Where(n => n.GetAttributeValue("class", "").Equals("tableSearchResults")).ToList();//gets table node
            var UrlList = table.First().Descendants("a").Where(n => n.GetAttributeValue("id","").Contains("search_result"));//gets list of urls within table
            foreach (HtmlNode i in UrlList)//iterates through each node and creates a new job with title and url
            {
                Job HtmlJob = new Job();
                HtmlJob.Url = i.GetAttributeValue("href", "");
                HtmlJob.Title = i.InnerText.Trim();
                Joblist.Add(HtmlJob);
            }
            Console.SetWindowSize(128, 32);
            foreach (Job JobNode in Joblist)//prints each job title with its respective url
            {
                Console.WriteLine(JobNode.Title);
                Console.WriteLine(JobNode.Url);
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }

    class Job
    {
        public string Title { get; set; }
        public string Url { get; set; }
    }
}
