//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MunicipalServicesApp.Classes.ViewModels
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// ViewModel for the Events and Announcements window, lots of logic here but keeps the code behond clean and simple.
    /// </summary>
    public class EventsAndAnnouncementsWindowViewModel : INotifyPropertyChanged
    {
        // Properties
        public ObservableCollection<Announcement> Announcements { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        /// <summary>
        /// The use of the ObservableCollection for events allows for dynamic updating of the UI when events are added or removed.
        /// </summary>
        private ObservableCollection<Event> events;
        public ObservableCollection<Event> Events
        {
            get => events;
            set
            {
                events = value;
                OnPropertyChanged(nameof(Events));
            }
        }

        /// <summary>
        /// This is a the collection of Recommended Events which is used in the UI to display the top 20 recommended events. 
        /// Explained more in ToggleInterest method.
        /// </summary>
        private ObservableCollection<Event> recommendedEvents;
        public ObservableCollection<Event> RecommendedEvents
        {
            get => recommendedEvents;
            set
            {
                recommendedEvents = value;
                OnPropertyChanged(nameof(RecommendedEvents));
            }
        }

        /// <summary>
        /// This is for the message that is displayed when no events are found based on the filter criteria. Again for dynamic updating of the UI.
        /// </summary>
        private string noEventsMessage;
        public string NoEventsMessage
        {
            get => noEventsMessage;
            set
            {
                noEventsMessage = value;
                OnPropertyChanged(nameof(NoEventsMessage));
            }
        }

        /// <summary>
        /// Used to help find date range when searching to allocate points in the toggle interest method.
        /// </summary>
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        /// <summary>
        /// Used to help find date range when searching to allocate points in the toggle interest method.
        /// </summary>
        private DateTime? endDate;
        public DateTime? EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        /// <summary>
        /// Used to store the selected category when searching to allocate points in the toggle interest method.
        /// </summary>
        private string selectedCategory;
        public string SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        // Commands

        //This is used to toggle the interested status of an event and update the recommended events. and is made possible by The RelayCommand in Helpers
        public ICommand ToggleInterestCommand { get; set; }

        // Private Fields
        private ObservableCollection<Event> originalEvents;
        private Dictionary<string, List<Event>> eventsByCategory;
        private Dictionary<DateTime, List<Event>> eventsByDate;
        private Dictionary<Event, int> eventScores;
        private Stack<string> searchHistory;

        // Constructor
        /// <summary>
        /// Initialising the announcements and events with some dummy data. Also intialising all the collections and commands.
        /// </summary>
        public EventsAndAnnouncementsWindowViewModel()
        {


            Announcements = new ObservableCollection<Announcement>
            {
                new Announcement { Title = "New Road Work", Date = DateTime.Now, Description = "Road work is scheduled for next week." },
                new Announcement { Title = "Library Opening", Date = DateTime.Now.AddDays(1), Description = "The new library will open tomorrow." },
                new Announcement { Title = "Community Meeting", Date = DateTime.Now.AddDays(2), Description = "Join us for a community meeting to discuss town improvements." },
                new Announcement { Title = "Water Supply Maintenance", Date = DateTime.Now.AddDays(3), Description = "Water supply will be interrupted for maintenance." },
                new Announcement { Title = "Park Renovation", Date = DateTime.Now.AddDays(4), Description = "The town park will be closed for renovation." },
                new Announcement { Title = "Fire Safety Drill", Date = DateTime.Now.AddDays(5), Description = "A fire safety drill will be conducted in the town hall." },
                new Announcement { Title = "New Recycling Program", Date = DateTime.Now.AddDays(6), Description = "A new recycling program will be introduced next month." },
                new Announcement { Title = "Street Cleaning Schedule", Date = DateTime.Now.AddDays(7), Description = "Street cleaning will take place every Monday." },
                new Announcement { Title = "Public Transport Update", Date = DateTime.Now.AddDays(8), Description = "New bus routes will be introduced next week." },
                new Announcement { Title = "Community Garden", Date = DateTime.Now.AddDays(9), Description = "A community garden will be established in the town park." },
                new Announcement { Title = "Town Hall Renovation", Date = DateTime.Now.AddDays(10), Description = "The town hall will be closed for renovation." },
                new Announcement { Title = "New Playground", Date = DateTime.Now.AddDays(11), Description = "A new playground will be built in the town park." },
                new Announcement { Title = "Electricity Maintenance", Date = DateTime.Now.AddDays(12), Description = "Electricity supply will be interrupted for maintenance." },
                new Announcement { Title = "New Sports Complex", Date = DateTime.Now.AddDays(13), Description = "A new sports complex will be built in the town." },
                new Announcement { Title = "Community Cleanup", Date = DateTime.Now.AddDays(14), Description = "Join us for a community cleanup event." },
                new Announcement { Title = "New School Opening", Date = DateTime.Now.AddDays(15), Description = "A new school will be opened next month." },
                new Announcement { Title = "Public Library Update", Date = DateTime.Now.AddDays(16), Description = "The public library will be closed for inventory." },
                new Announcement { Title = "New Health Center", Date = DateTime.Now.AddDays(17), Description = "A new health center will be opened next month." },
                new Announcement { Title = "Community BBQ", Date = DateTime.Now.AddDays(18), Description = "Join us for a community BBQ in the town park." },
                new Announcement { Title = "New Fire Station", Date = DateTime.Now.AddDays(19), Description = "A new fire station will be built in the town." }
            };

            Events = new ObservableCollection<Event>
            {
                new Event { Title = "Music Festival", Date = new DateTime(2024, 10, 12), Category = "Music/Culture", Description = "Join us for the annual music festival.", Location = "City Park", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Charity Run", Date = new DateTime(2024, 10, 22), Category = "Health/Fitness", Description = "A 5k run for charity.", Location = "Town Center", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Art Exhibition", Date = new DateTime(2024, 11, 5), Category = "Art/Exhibition", Description = "Local artists will showcase their work.", Location = "Art Gallery", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Food Fair", Date = new DateTime(2024, 11, 10), Category = "Food/Drink", Description = "Enjoy a variety of food from local vendors.", Location = "Market Square", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Book Fair", Date = new DateTime(2024, 11, 15), Category = "Books/Literature", Description = "A fair for book lovers.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Science Fair", Date = new DateTime(2024, 12, 1), Category = "Science/Technology", Description = "A fair showcasing scientific projects.", Location = "Science Museum", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Theater Play", Date = new DateTime(2024, 12, 15), Category = "Theater/Drama", Description = "A local theater group will perform a play.", Location = "Town Theater", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Dance Competition", Date = new DateTime(2024, 12, 20), Category = "Dance/Performance", Description = "A dance competition for all ages.", Location = "Community Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Cooking Class", Date = new DateTime(2025, 1, 5), Category = "Food/Drink", Description = "Learn to cook with local chefs.", Location = "Culinary School", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Yoga Session", Date = new DateTime(2025, 1, 12), Category = "Health/Fitness", Description = "A yoga session in the town park.", Location = "Town Park", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Photography Workshop", Date = new DateTime(2025, 1, 20), Category = "Art/Exhibition", Description = "Learn photography with local photographers.", Location = "Art Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Wine Tasting", Date = new DateTime(2025, 2, 5), Category = "Food/Drink", Description = "Taste a variety of wines from local wineries.", Location = "Winery", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Tech Conference", Date = new DateTime(2025, 2, 15), Category = "Science/Technology", Description = "A conference on the latest in technology.", Location = "Convention Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Poetry Reading", Date = new DateTime(2025, 3, 1), Category = "Books/Literature", Description = "Local poets will read their work.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Fitness Bootcamp", Date = new DateTime(2025, 3, 10), Category = "Health/Fitness", Description = "A fitness bootcamp for all levels.", Location = "Sports Center", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Craft Fair", Date = new DateTime(2025, 3, 20), Category = "Art/Exhibition", Description = "A fair showcasing local crafts.", Location = "Fairgrounds", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Film Festival", Date = new DateTime(2025, 4, 1), Category = "Music/Culture", Description = "A festival showcasing local films.", Location = "Cinema", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Marathon", Date = new DateTime(2025, 4, 15), Category = "Health/Fitness", Description = "A marathon through the town.", Location = "City Streets", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Jazz Concert", Date = new DateTime(2025, 4, 25), Category = "Music/Culture", Description = "A jazz concert in the town park.", Location = "Town Park", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Baking Competition", Date = new DateTime(2025, 5, 1), Category = "Food/Drink", Description = "A baking competition for all ages.", Location = "Community Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Robotics Workshop", Date = new DateTime(2025, 5, 15), Category = "Science/Technology", Description = "Learn about robotics with local experts.", Location = "Tech Hub", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Drama Workshop", Date = new DateTime(2025, 6, 1), Category = "Theater/Drama", Description = "A workshop on drama and acting.", Location = "Theater Hall", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Sculpture Exhibition", Date = new DateTime(2025, 6, 15), Category = "Art/Exhibition", Description = "An exhibition of local sculptures.", Location = "Art Museum", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Health Fair", Date = new DateTime(2025, 7, 1), Category = "Health/Fitness", Description = "A fair promoting health and wellness.", Location = "Health Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Literary Festival", Date = new DateTime(2025, 7, 15), Category = "Books/Literature", Description = "A festival celebrating literature.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Rock Concert", Date = new DateTime(2025, 8, 1), Category = "Music/Culture", Description = "A rock concert in the town square.", Location = "Town Square", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Cooking Competition", Date = new DateTime(2025, 8, 15), Category = "Food/Drink", Description = "A cooking competition for all ages.", Location = "Community Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Science Workshop", Date = new DateTime(2025, 9, 1), Category = "Science/Technology", Description = "A workshop on various scientific topics.", Location = "Science Lab", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Dance Workshop", Date = new DateTime(2025, 9, 15), Category = "Dance/Performance", Description = "A workshop on different dance styles.", Location = "Dance Studio", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Art Auction", Date = new DateTime(2025, 10, 1), Category = "Art/Exhibition", Description = "An auction of local art pieces.", Location = "Art Gallery", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Fitness Challenge", Date = new DateTime(2025, 10, 15), Category = "Health/Fitness", Description = "A fitness challenge for all levels.", Location = "Sports Center", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Book Club Meeting", Date = new DateTime(2025, 11, 1), Category = "Books/Literature", Description = "A meeting of the local book club.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Classical Concert", Date = new DateTime(2025, 11, 15), Category = "Music/Culture", Description = "A classical music concert.", Location = "Concert Hall", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Food Festival", Date = new DateTime(2025, 12, 1), Category = "Food/Drink", Description = "A festival celebrating local food.", Location = "Town Square", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Tech Workshop", Date = new DateTime(2025, 12, 10), Category = "Science/Technology", Description = "A workshop on the latest in technology.", Location = "Tech Hub", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Poetry Slam", Date = new DateTime(2025, 12, 15), Category = "Books/Literature", Description = "A poetry slam event.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Yoga Retreat", Date = new DateTime(2025, 12, 20), Category = "Health/Fitness", Description = "A yoga retreat in the town park.", Location = "Town Park", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Craft Workshop", Date = new DateTime(2025, 12, 25), Category = "Art/Exhibition", Description = "A workshop on various crafts.", Location = "Community Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Film Screening", Date = new DateTime(2025, 12, 30), Category = "Music/Culture", Description = "A screening of local films.", Location = "Cinema", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Charity Walk", Date = new DateTime(2026, 1, 5), Category = "Health/Fitness", Description = "A charity walk through the town.", Location = "City Streets", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Blues Concert", Date = new DateTime(2026, 1, 10), Category = "Music/Culture", Description = "A blues concert in the town park.", Location = "Town Park", EventType = "Outdoor", IsInterested = false },
                new Event { Title = "Cooking Demo", Date = new DateTime(2026, 1, 15), Category = "Food/Drink", Description = "A cooking demonstration by local chefs.", Location = "Community Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Tech Expo", Date = new DateTime(2026, 1, 20), Category = "Science/Technology", Description = "An expo showcasing the latest in technology.", Location = "Convention Center", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Book Signing", Date = new DateTime(2026, 1, 25), Category = "Books/Literature", Description = "A book signing event with local authors.", Location = "Library", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Fitness Expo", Date = new DateTime(2026, 1, 30), Category = "Health/Fitness", Description = "An expo promoting fitness and wellness.", Location = "Sports Complex", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Art Gallery Opening", Date = new DateTime(2026, 2, 5), Category = "Art/Exhibition", Description = "The opening of a new art gallery.", Location = "Art Museum", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Dance Recital", Date = new DateTime(2026, 2, 10), Category = "Dance/Performance", Description = "A dance recital by local dancers.", Location = "Dance Studio", EventType = "Indoor", IsInterested = false },
                new Event { Title = "Food Truck Festival", Date = new DateTime(2026, 2, 15), Category = "Food/Drink", Description = "A festival featuring local food trucks.", Location = "City Center", EventType = "Outdoor", IsInterested = false }
                };

            RecommendedEvents = new ObservableCollection<Event>();

            originalEvents = new ObservableCollection<Event>(Events);
            eventsByCategory = new Dictionary<string, List<Event>>();
            eventsByDate = new Dictionary<DateTime, List<Event>>();
            searchHistory = new Stack<string>();


            //Populating the events by category and date dictionaries.
            OrganizeEvents();
            //This adds the "any" option in to the drop down for category search.
            PopulateCategories();
            //Setting Score for all events to 0.
            eventScores = new Dictionary<Event, int>();
            foreach (var ev in Events)
            {
                eventScores[ev] = 0;
            }

            ToggleInterestCommand = new RelayCommand<Event>(ToggleInterest);
        }

        /// <summary>
        /// Poulates the list in the event tab with the events that are in the selected category.
        /// </summary>
        private void OrganizeEvents()
        {
            foreach (var ev in Events)
            {
                if (!eventsByCategory.ContainsKey(ev.Category))
                {
                    eventsByCategory[ev.Category] = new List<Event>();
                }
                eventsByCategory[ev.Category].Add(ev);

                if (!eventsByDate.ContainsKey(ev.Date.Date))
                {
                    eventsByDate[ev.Date.Date] = new List<Event>();
                }
                eventsByDate[ev.Date.Date].Add(ev);
            }
        }

        /// <summary>
        /// Adds the "Any" option to the category dropdown.
        /// </summary>
        private void PopulateCategories()
        {
            Categories = new ObservableCollection<string>(eventsByCategory.Keys);
            Categories.Insert(0, "Any");
        }

        /// <summary>
        /// This is the algorithm that I came up with to recommend events based on the user's interests.
        /// If you click I'm interested on an event, all events with the same category get 2 points,
        /// all events with the same type (indoor/outdoor) get 2 points, and all events in the same month get 2 points.
        /// After that if you search for a category, all events in that category get 1 point.
        /// And if you search for a date range, all events in that range get 1 point.
        /// Therefore the top scoring events are the recommended ones.
        /// I chose to only diplsay the top 20 recommended events.
        /// </summary>
        /// <param name="ev"></param>
        public void ToggleInterest(Event ev)
        {
            ev.IsInterested = !ev.IsInterested;

            if (ev.IsInterested)
            {
                // Add 2 points for all events in the same category
                if (eventsByCategory.ContainsKey(ev.Category))
                    eventScores[ev] += 2;

                // Add 2 points for every event with the same type
                foreach (var e in events.Where(e => e.EventType == ev.EventType))
                {
                    eventScores[e] += 2;
                }

                // Add 2 points for every event in the same month
                foreach (var e in events.Where(e => e.Date.Month == ev.Date.Month))
                {
                    eventScores[e] += 2;
                }
            }
            else //If the user unchecks the interested box, the points are removed.
            {
                // Subtract 2 points for category
                if (eventsByCategory.ContainsKey(ev.Category))
                    eventScores[ev] -= 2;

                // Subtract 2 points for every event with the same type
                foreach (var e in events.Where(e => e.EventType == ev.EventType))
                {
                    eventScores[e] -= 2;
                }

                // Subtract 2 points for every event in the same month
                foreach (var e in events.Where(e => e.Date.Month == ev.Date.Month))
                {
                    eventScores[e] -= 2;
                }
            }
            //Updating UI
            OnPropertyChanged(nameof(Events));
            SortEventsByScore();
        }

        /// <summary>
        /// Adding to the search historu and then searching for events based on the category. And assigning points.
        /// </summary>
        /// <param name="category"></param>
        public void SearchEventsByCategory(string category)
        {
            searchHistory.Push($"Category: {category}");

            if (category == "Any")
            {
                Events = new ObservableCollection<Event>(originalEvents);
            }
            else if (eventsByCategory.ContainsKey(category))
            {
                foreach (var ev in eventsByCategory[category])
                {
                    eventScores[ev] += 1;  // Add 1 point for each event in the searched category
                }
                SortEventsByScore();
                Events = new ObservableCollection<Event>(eventsByCategory[category]);
            }
            else
            {
                Events = new ObservableCollection<Event>();
            }
            // If no events are found, display a message.
            NoEventsMessage = Events.Count == 0 ? "No events found, try changing filters." : string.Empty;
        }
        /// <summary>
        /// Method to search by date range and assign points.
        /// ChatGPT helped me here to do this efficiently and cleanly (see line 342-343).
        /// </summary>
        public void SearchEventsByDateRange(DateTime startDate, DateTime endDate)
        {
            searchHistory.Push($"Date Range: {startDate.ToShortDateString()} - {endDate.ToShortDateString()}");
            var eventsInRange = Events.Where(e => e.Date.Date >= startDate.Date && e.Date.Date <= endDate.Date).ToList();

            foreach (var ev in eventsInRange)
            {
                eventScores[ev] += 1;  // Add 1 point for each event in the searched date range
            }
            //Updating UI
            Events = new ObservableCollection<Event>(eventsInRange);
            SortEventsByScore();
            //If no events are found, display a message.
            NoEventsMessage = Events.Count == 0 ? "No events found, try changing filters." : string.Empty;
        }

        /// <summary>
        /// Cleaing all the filters and showing the original unfiltered list of events.
        /// </summary>
        public void ClearFilters()
        {
            SelectedCategory = null;
            StartDate = null;
            EndDate = null;

            Events = new ObservableCollection<Event>(originalEvents);

            OnPropertyChanged(nameof(Events));
        }

        /// <summary>
        /// Sorting the recommended events by score and displaying the top 20.
        /// </summary>
        public void SortEventsByScore()
        {
            RecommendedEvents = new ObservableCollection<Event>(
                originalEvents.OrderByDescending(ev => eventScores[ev]).Take(20)
            );
            OnPropertyChanged(nameof(RecommendedEvents));
        }

        /// <summary>
        /// For UI Updating.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
    //==============================================================[END OF CLASS]============================================================== 

}
//==============================================================[END OF FILE]==============================================================
