using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderSecurityPermission
{
    class Program
    {
        static void Main(string[] args)
        {
            // SETTING FOLDER SECURITY PERMISSIONS

            Console.Write("Please specify the name of your directory: ");   // Ask user for folder name to be created
            string input = Console.ReadLine();                              // Store input to variable

            Controller.ValidateName(ref input);                             // Call the method to validate name to make sure it's not null or empty

            string path = @"c:\TestDirectory\" + input;                     // Specify the directory you want to manipulate - C drive on the TestDirectory folder

            // FOLDER CREATION
            try
            {
                // Check whether the directory exists
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path already exists.");
                    Console.Write("Do you want to delete the path? (y/n): ");
                    string resp = Console.ReadLine();

                    // Ask user to put a valid selection
                    while (resp != "y" && resp != "n")
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // change text color to red
                        Console.Write("Invalid Selection \nDo you want to delete the path? (y/n): ");
                        resp = Console.ReadLine();  // resp = response from the user
                    } // end while

                    Console.ForegroundColor = ConsoleColor.Green;   // Change text color back to green

                    if (resp.Equals("y"))
                    {
                        Directory.CreateDirectory(path).Delete();   // delete directory
                        Console.WriteLine("The directory was deleted successfully.");
                    } // end if

                    return;

                } // end if

                // Create the directory
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

            } // end try
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The process failed: {0}", e.ToString());
            } // end catch

            // Set permissions for the file created
            Console.Write("Do you want to set permissions on {0}? (y/n): ", input);
            string resp2 = Console.ReadLine();

            // Validate response to make sure it only accepts "y" and "n"
            while (resp2 != "y" && resp2 != "n")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Invalid selection. \nDo you want to set permissions on {0}? (y/n): ", input);
                resp2 = Console.ReadLine();
            } // end while

            // SET SECURITY PERMISSIONS
            if (resp2.Equals("y"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Setting permissions for {0}.", input);
                Console.Write("RE = Read and Execute \nR = Read \nM = Modify \nW = Write \nF = Full Control \nWhat permissions does {0} have? ", input);
                string UserRights = Console.ReadLine();

                // Validate to make sure user is only inputting the right variable (case sensitive)
                while (UserRights != "RE" && UserRights != "R" && UserRights != "M" && UserRights != "W" && UserRights != "F")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Invalid Section. \nRE = Read and Execute \nR = Read \nM = Modify \nW = Write \nF = Full Control \nWhat permissions does {0} have? ", input);
                    UserRights = Console.ReadLine();
                } // end while

                Console.ForegroundColor = ConsoleColor.Green;

                // conditional statements to choose what permission to set
                if (UserRights == "RE")
                {
                    Controller.SetFolderPermissionReadAndExecute(path);
                    Console.WriteLine("{0} has Read and Execute permission.", input);
                }
                else if (UserRights == "R")
                {
                    Controller.SetFolderPermissionRead(path);
                    Console.WriteLine("{0} has Read permission.", input);
                }
                else if (UserRights == "M")
                {
                    Controller.SetFolderPermissionModify(path);
                    Console.WriteLine("{0} has Modify permission.", input);
                }
                else if (UserRights == "W")
                {
                    Controller.SetFolderPermissionWrite(path);
                    Console.WriteLine("{0} has Write permission.", input);
                }
                else if (UserRights == "F")
                {
                    Controller.SetFolderPermissionFullControl(path);
                    Console.WriteLine("{0} has Full Control permission.", input);
                } // end else if

            } // end if

        } // end Main method
    } // end Program
} // end namespace
