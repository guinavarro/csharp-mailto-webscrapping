// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

Console.WriteLine("Input the url with http prefix:");
var url = Console.ReadLine();
var regex = new Regex(@"<a\s+href=""mailto:([^""]+)"">[^<]+</a>");

var stringBuilder = new StringBuilder();
var client = new WebClient();

var htmlString = client.DownloadString(url);
var matches = regex.Matches(htmlString);
var emailCount = 0;

if (matches.Count == 0)
{
    Console.WriteLine("Sorry, we couldn't be able to find any 'mailto' in this website.");
    return;
}

foreach (Match match in matches)
{
    stringBuilder.AppendLine(match.Groups[1].Value);
    emailCount++;
}

string currentDirectory = Environment.CurrentDirectory;
string outputFilePath = Path.Combine(currentDirectory, "arquivos", "emails.txt");

Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
File.WriteAllText(outputFilePath, stringBuilder.ToString());

Console.WriteLine($"{emailCount} e-mails saved in " + outputFilePath);

