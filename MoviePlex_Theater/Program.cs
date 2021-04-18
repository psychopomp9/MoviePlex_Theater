using System;
using System.Linq;
using System.Threading;

namespace MoviePlex_Theater
{
    class Program
    {
        static string[] ordinalNumbers = { "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Ninth", "Tenth" };
        static string[] movieNames;
        static string[] movieRatings;
        static string[] ratingsArr = { "G", "PG", "PG-13", "R", "NC-17" };

        static void Main(string[] args)
        {
            //call MainScreen Function
            MainScreen();
        }
        static void Welcome_msg()
        {
            ///diplay welcome message at the center of console screen in blue foreground
            Console.ForegroundColor = ConsoleColor.Blue;
            string textToEnter = "************************************";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            textToEnter = "*** Welcome to MoviePlex Theater ***";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            textToEnter = "************************************";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (textToEnter.Length / 2)) + "}", textToEnter));
            Console.ResetColor(); //will reset colors to default (while foreground and black background)
        }
        static void MainScreen()
        {
            //call the welcome_msg function
            Welcome_msg();

            int uSelection = 0;
            do
            {
                try
                {
                    //asks user to select admin or guest
                    Console.WriteLine("\nPlease Select From the Following Options: ");
                    Console.WriteLine("1. Administrator \n2. Guest");
                    Console.Write("\nSelection: ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    uSelection = Convert.ToInt32(Console.ReadLine());
                    Console.ResetColor();
                    
                    //determins which option user select
                    switch (uSelection)
                    {
                        case 1:
                            //clear console and call Admin function if 1 selected
                            Console.Clear();
                            Admin();
                            break;
                        case 2:
                            //clear console and call Guest function if 2 is selected
                            Console.Clear();
                            Guest();
                            break;
                        default:
                            //for other options it'll print to select above options in red color
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("please select from above options");
                            Console.ResetColor();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    //catches exception if user enter values that aren't integer and display msg in red color
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("please select from above options");
                    Console.ResetColor();
                }
            } while (uSelection != 1 && uSelection != 2); //run till user enters 1 or 2
            


            Console.ReadLine();
        }

        static void Admin()
        {
            try
            {
                //Verify if Password entered by Asmin is correct
                char passwordVerification = PasswordVerification("password");

                //Switch case for password verification
                switch (passwordVerification)
                {
                    case 'A':
                        Console.Clear();
                        //If Password entered is correct Go to MovieDetails
                        MovieDetails();
                        break;
                    case 'B':
                        Console.Clear();
                        //If Admin enters B, go back to main screen
                        MainScreen();
                        break;
                    case 'M':
                        //If Admin enters incorrect password more than 5 times
                        //show error message and return to main screen
                        for (int i = 5; i > 0; i--)
                        {
                            Console.Clear();
                            Console.WriteLine(" You have exceeded maximum amount of attempts. You will be redirected back to main screen in {0}.", i);
                            Thread.Sleep(1000);
                        }
                        Console.Clear();
                        MainScreen();
                        break;
                }
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                //catches exception if any
                Console.WriteLine("\n Sorry for the inconvenience. You will be redirected to Home Screen.");
                Thread.Sleep(3000);
                Console.ResetColor();
            }
            
        }

        static void Guest()
        {
            //Console.WriteLine("Guest Selected!");

            Welcome_msg(); //call welcome_msg function

            // checks if movieNames array is null or not
            if (movieNames != null)
            {
                //if movieNames array has values
                Console.WriteLine("\n\nThere are {0} movies playing today. Please choose from the following movies: ", movieNames.Length);

                //print movie names and rating
                for (int i = 0; i < movieNames.Length; i++)
                {
                    Console.WriteLine("\t{0}. {1} {2}", (i + 1), movieNames[i], ("{" + movieRatings[i] + "}"));
                }

                int uMovieSel = 0;
                do
                {
                    try
                    {
                        //asks user to select a movie they want to watch
                        Console.Write("\nWhich movie would you like to watch : ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        uMovieSel = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();

                        //checks if user input is not less than or equals 0 and it's not more than the displayed options 
                        if (uMovieSel > movieNames.Length || uMovieSel <= 0)
                        {
                            //if it's true display msg to choose from above option
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("please select from above options");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception ex)
                    {
                        //catches exception if enters other values than integer
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("please select from above options");
                        Console.ResetColor();
                    }
                } while (uMovieSel <= 0 || uMovieSel > movieNames.Length); //runs till proper input entered

                int uAge = 0;
                do
                {
                    try
                    {
                        //asks user for age to verify
                        Console.Write("Please enter your age for verification: ");
                        Console.ForegroundColor = ConsoleColor.Blue;
                        uAge = Convert.ToInt32(Console.ReadLine());
                        Console.ResetColor();
                        //checks if age is not 0 or less and not more than 120
                        if (uAge < 1 || uAge > 120)
                        {
                            //if it's then display this msg in red foreground
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("please enter proper age between 1 and 120");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception ex)
                    {
                        //catches error for unappropriate input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("please enter proper age between 1 and 120");
                        Console.ResetColor();
                    }
                } while (uAge < 1 || uAge > 120); //runs till user enter age between 1 and 120

                int movieFlag = 0;
                int age = 0;

                //checks if user selected movie has rating or age
                foreach (string r in ratingsArr)
                {
                    if (movieRatings[uMovieSel - 1].Equals(r))
                    {
                        //if it's a rating and increase movieFlag
                        movieFlag++;
                    }
                }

                //checks if movieFlag is 0 or not
                if(movieFlag == 0)
                    //for 0 it'll convert string rating to integer
                    age = Convert.ToInt32(movieRatings[uMovieSel - 1]);
                else
                {
                    //for other(1) it'll check rating and assign age based on rating
                    switch (movieRatings[uMovieSel - 1])
                    {
                        case "G":
                            age = 1;
                            break;
                        case "PG":
                            age = 12;
                            break;
                        case "PG-13":
                            age = 12;
                            break;
                        case "R":
                            age = 17;
                            break;
                        case "NC-17":
                            age = 17;
                            break;
                        default:
                            age = 0;
                            break;
                    }
                }

                //checks if user age is above the age to watch the movie
                if (uAge > age)
                {
                    //if age is above then print Enjoy movie in green
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\nEnjoy Your Movie with a POPCORN!");
                    Console.ResetColor();
                    char uSelect2 = '0';
                    do
                    {
                        try
                        {
                            //asks user to enter M/m or S/s to go back to specific page
                            Console.WriteLine("\n\nPree M to go back to Guest Main Menu.\nPress S to go back to Start Page.");
                            Console.Write("Please Select: ");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            uSelect2 = Convert.ToChar(Console.ReadLine());
                            Console.ResetColor();
                            //checks where user wants to go
                            switch (Char.ToUpper(uSelect2))
                            {
                                case 'M':
                                    //for m/M it'll call Guest function after clearing console
                                    Console.Clear();
                                    Guest();
                                    break;
                                case 'S':
                                    //for s/S it'll call MainScreen function after clearing console
                                    Console.Clear();
                                    MainScreen();
                                    break;
                                default:
                                    //for other selections it'll display following message in red foreground
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("please select from above options");
                                    Console.ResetColor();
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            //catches error if user input string and display msg in red foreground
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("please select from above options");
                            Console.ResetColor();
                        }
                    } while (uSelect2 != 'M' && uSelect2 != 'S'); //runs till user enters m/M/s/S
                }
                else
                {
                    //age is not more than watchable age it'll display following msg in red
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\nYour age doesn't match the required age to watch this movie!");
                    Console.ResetColor();
                    //display following msg in blue
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("You will be redirected to Guest Screen in few seconds. Please Standby.");
                    Console.ResetColor();
                    
                    //call Guest after waiting for 8 second and clearing console
                    Thread.Sleep(8000);
                    Console.Clear();
                    Guest();
                }
            }
            else
            {
                //redirect to mainScreen after 5 seconds and clearing console
                for (int i = 5; i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine("Sorry, There aren't any movies playing today.\nYou will be redirected back to main screen in {0}.", i);
                    Thread.Sleep(1000);
                }
                Console.Clear();
                MainScreen();
            }
        }

        //Function to verify password
        static char PasswordVerification(string correctPassword)
        {
            int count = 5;
            Console.Write(" Please Enter Admin Password: ");
            Console.ForegroundColor = ConsoleColor.Blue;
            string userPassword = Console.ReadLine();
            Console.ResetColor();
            while (count > 0)
            {
                //If password is correct return A
                if (userPassword.Trim().Equals(correctPassword))
                {
                    return 'A';
                }
                else
                {
                    //If password is incorrect count attempts
                    count--;
                    Console.Write(" \n\nYou Entered an Invalid Password!");
                    Console.Write(" You have {0} more attempts to enter the correct password OR Press B to go back to previous screen.",count);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    userPassword = Console.ReadLine();
                    Console.ResetColor();
                    if (userPassword.Trim().ToLower().Equals("b"))
                    {
                        return 'B';
                    }
                }
            }
            return 'M';

        }

        //Function to Input Movie Details
        static void MovieDetails()
        {
            try
            {
                Console.Clear();
                Console.WriteLine(" Welcome to MoviePlex Administrator");
                Console.Write(" How many Movies are you playing today?: ");
                bool flag = false;
                Console.ForegroundColor = ConsoleColor.Blue;
                string moviesCount = Console.ReadLine();
                Console.ResetColor();
                int numberOfMovies;

                while(!flag)
                {
                    //Check if input is Integer
                    if (int.TryParse(moviesCount, out numberOfMovies))
                    {
                        //Convert input to int
                        numberOfMovies = int.Parse(moviesCount);
                        if (numberOfMovies >= 0 && numberOfMovies <= 10)
                        {
                            //Enter movie details if input is valid
                            EnterMovieDetails(numberOfMovies);
                            flag = true;
                        }
                        else
                        {
                            //If input is invalid ask for valid input
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\n Please enter a valid number between 1 and 10: ");
                            Console.ResetColor();
                            Console.ForegroundColor = ConsoleColor.Blue;
                            moviesCount = Console.ReadLine();
                            Console.ResetColor();

                        }
                    }
                    else
                    {
                        //If input is not int, ask for valid input
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n Please enter a valid number between 1 and 10: ");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        moviesCount = Console.ReadLine();
                        Console.ResetColor();
                    }
                }
            }
            catch(Exception e)
            {
                //Catch exception if any
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Sorry for the inconvenience. You will be redirected to Home Screen.");
                Thread.Sleep(3000);
                Console.ResetColor();
            }
        }

        //Enter movie Details
        static void EnterMovieDetails(int numberOfMovies)
        {
            try
            {
                //Initialize movie and rating arrays
                movieNames = new string[numberOfMovies];
                movieRatings = new string[numberOfMovies];

                string movieName, movieRating;
                for (int i = 0; i < numberOfMovies; i++)
                {
                    Console.Write("\n Please Enter the {0} Movies name: ", ordinalNumbers[i]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    movieName = Console.ReadLine().Trim();
                    Console.ResetColor();

                    
                    Console.Write(" Please Enter Age Limit OR Rating For the {0} Movie: ", ordinalNumbers[i]);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    movieRating = Console.ReadLine().Trim().ToUpper();
                    Console.ResetColor();

                    //Verify if movie name and rating is valid and add the values to respective arrays
                    if (movieRating != "" && movieName != "" && MovieRatingVerification(movieRating))
                    {
                        movieNames[i] = movieName;
                        movieRatings[i] = movieRating;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("\n Please Enter Valid {0} Movie Name and Rating from [{1}] or Age between 1 and 120.\n", ordinalNumbers[i], string.Join(", ", ratingsArr));
                        Console.ResetColor();
                        i--;
                    }
                }

                //Display Movie and Ratings
                bool flag = false;
                do
                {
                    Console.WriteLine();
                    for (int i = 0; i < numberOfMovies; i++)
                    {
                        Console.WriteLine("\t{0}. {1} {2}", (i + 1), movieNames[i], ("{"+movieRatings[i]+"}"));
                    }
                    //Ask if Admin wants to reenter movies or return to Main Screen
                    Console.Write(" \n Your Movies Playing Today are Listed Above. Are you Satisfied? (Y/N): ");
                    Console.ForegroundColor = ConsoleColor.Blue;
                    string answer = Console.ReadLine();
                    Console.ResetColor();
                    
                    if (answer.Trim().ToLower() == "y")
                    {
                        Console.Clear();
                        MainScreen();
                    }
                    else if (answer.Trim().ToLower() == "n")
                    {
                        MovieDetails();
                    }
                    else
                    {
                        //Ask Admin to reenter if input is invalid
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\n Please Enter a Valid Input!!!");
                        Console.ResetColor();
                        flag = true;
                    }
                } while (flag);
            }
            catch(Exception e)
            {
                //Catch exception if any
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Sorry for the inconvenience. You will be redirected to Home Screen.");
                Thread.Sleep(3000);
                Console.ResetColor();
            }
        }

        //Verify Movie rating is valid
        static bool MovieRatingVerification(string rating)
        {
            try
            {
                //Verify if rating is present in array
                if (ratingsArr.Contains(rating))
                {
                    return true;
                }
                //Else verify if age is b/w 1 and 120
                else if(Convert.ToInt32(rating) > 0 && Convert.ToInt32(rating) <= 120)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
