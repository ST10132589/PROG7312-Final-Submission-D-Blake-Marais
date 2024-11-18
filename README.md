# Daniel Blake Marais ST10132589 ô¿ô

To get this wonderful project to compile in Visual Studio Community will be a bit finnicky as the file structure is all messed up. 
My old SSD got corrupted midway between Part 2 and 3's submissions. I only realised the folder would be an issue when I initially uploaded it to GitHub. Please be patient! And follow Closely!!

***HERE IS THE DEMO VID, I SHOW HOW YOU INSTALL IT HERE AS WELL*** [Youtube Video](https://www.youtube.com/watch?v=tcL0homkH8U&ab_channel=Fourge) 

## Here are the steps
-  Make sure your Visual Studio Community is up to date.
-  Click on the Green 'Code Button' on the repository.
-  Select 'HTTPS' and copy the link ![image](https://github.com/user-attachments/assets/c5635d29-1173-4582-a0d0-7e757f95bb9e)
-  Now Open Visual Studio Community.
-  Click on 'Clone a Repository'. ![image](https://github.com/user-attachments/assets/c7950bf1-c40e-4a5e-be19-c5d263568837)

-  Paste in the link. ![image](https://github.com/user-attachments/assets/c4550b1e-0691-4b78-8b6c-0999939824ce)

## **There will be an issue with the configuration so follow these next steps closely!**
-  Click Clone and wait.
-  In the Solution Explorer you will be presented with this: ![image](https://github.com/user-attachments/assets/54da7e83-96f3-48b0-8ce8-972fb9b0e044)
# ***MAKE SURE TO DOUBLE CLICK ON THE GREEN ONE [MunicipalServicesApp.sln (MunicipalServicesApp.sln)]***
- ***DO NOT SELECT THE ORANGE (NESTED) ONE [MunicipalServicesApp.sln (MunicipalServicesApp\MunicipalServicesApp.sln)]***
-  Once its loaded, go Ahead and click start and have fun!
- **NB: You can also navigate to the bin folder and running the app itself without Visual Studio by going to [WhereThisIsSaved\PROG7312-Final-Submission-D-Blake-Marais-2\MunicipalServicesApp\bin] and double clicking the 'Municipal Services App' .exe**

# Overview of the App
I will give a brief overview of the app and how each page/window works.
Once you run the app you will be presented with the four buttons representing the headings below: Report Issues, View Saved Reports, Local Events and Announcements and Service Request Status.
## Report Issues
### First Page
- When you click on Report Issues you begin the report creation process
- On the first page you are presented with a textbox for Location input and a combo box for Issue type.
- When you're done typing in the textbox, hit enter and it will select the combobox and update the progress bar.
- Once you select an issue type, you can always change the option or clear it using the button. Once again the progress bar updates.
- When both fields are filled in, the app lets you proceed to the next page using the button.
### Second Page
- Here you can enter in the description, attach an image and select the priority.
- The description is in a rich text box and you can also click enter to update the progress bar.
- Attach an image using the 'Click to attach file' button and see a preview of it once you attach it. Once again the progress bar updates.
- And finally, added in part three, you can indicate priority (This is where I use Heaps). Once again the progress bar updates.
- Once all fields have been saved go ahead and click the 'Submit Report' button where you will be presented with a summary window.
- Your report is now saved in View Saved Reports
## View Saved Reports
- If you have already created a report it will show here, if not you will be notified that no reports have been saved.
- You wl be presented with a List of reports, with their ID, description and priority.
- You can select one from the list and click the 'View Selected Report' button to view that same summary window from Report Issues.
- You can also order by priority (very efficient with the heaps)
## Local Events and Announcements
### Announcements
- Once you open the Local Events  window you'll be presented with the Local Announcements tab. Here you can scroll through and see all of the local announcements, their title, their date, and their description.
-  At the bottom of the page which is the same for all the tabs, you will see a back to main menu button.
### Events
- If you click on the events tab at the top you'll be greeted with all of the events which are all given a unique color (based on category) as well as filtering options.
- You can filter the Events by category using the Select Category drop down.
- You can filter between dates using the two Date Pickers.
- You can also toggle your interest per event by ticking the 'I'm Interested' Button.
- Once you toggle the I'm interested button it will update your recommended events.
- Your recommended events updated as well as when you search for a category or if you search between dates.
### Recommended events. 
- Here you'll see your most recommended events in order of events most tailored to your searches and Interest.
## Service Request Status
- Once you open the service request status window, you are greeted by a data grid as well as a menu on the right side with buttons, a search box and a combo box.
- The data grid on the left contains all of the service requests which contains their ID, description, category, priority, and status.
- You can click on any of the requests and once it is selected it turns yellow, for clarity sake.
- Once it is selected, you can click the 'View Details of Selected Request" Button
- This will bring up a pop-up-like winodw which shows all the details of the request as well as all of it's dependencies (And their info as well).
- You can use the 'Search by ID' text box to search for a request by its ID. When you click search, this will also bring up the view details pop-up.
- Order by. Here you can select different methods to order the data by using the drop-down and then once you click reorder, it will reorder it according to the parameter selected.
- Finally, the 'View Dependency Graph' button. Click on this and you will be able to view the dependency graph of all of the requests.
## Dependency Graph
- After you click the 'View Dependency Graph' button, you'll be presented with the Dependency Graph Window.
- Here you'll be presented with a horizontal scroll view, which has a graphical representation of all of the service requests as well as its dependencies.
- At the bottom of the Window, there is a key indicating the status of each of the requests represented by colours.
- You can use the scroll bar at the bottom to scroll from the first request all the way to the last.
- The first five requests have three dependencies, the next five have two dependencies, the next five have one dependency, and the last five have zero.
- Below each "Line" of requests it displays the number of dependencies the original Request has.
- There is also a 'Back to Service Request' button at the bottom to navigate back to the Service Requests window.

# Implementation of Trees
All three trees are populated in the ServiceRequestViewModel when the ServiceRequests are created
## Binary Tree
- The Basic Binary Tree is used to simply store all of the requests and bind them to the DataGrid. I can tell this is much more efficient than using a list, because I use a list for the events/announcements and the load time is considerably higher (especially when filtering/ordering by)
- It is also used as it has faster load times compared to lists, especially as the dataset grows,
- Simplified hierarchical organization of service requests,
- and it facilitates efficient traversal for binding and displaying data in the UI.
## Binary Search Tree
- This is used to search By ID.
- The BST's in-order traversal automatically arranges service requests in ascending order of their IDs.
- This ensures data is displayed in a sorted manner without requiring additional sorting operations.
- Searching by ID in a BST takes 𝑂(log⁡𝑛) time on average, as opposed to 𝑂(𝑛) in a linear search through a list[^1].
## AVL Tree
- The AVL Tree is used to order by category, as the tree is self balancing, this makes easy to fetch all of the categories and reorder the DataGrid as...
- Categories can have uneven distributions of service requests. The self-balancing nature of the AVL Tree ensures that these imbalances do not degrade performance.
- Efficient ordering is vital for smooth interaction with the DataGrid when users reorder service requests by category.
# Implementation of Graphs & Heaps
## Graph & Graph Traversal
- The Graph functionality is used in the Dependency Graph Diagram and is crucial for allowing an efficient and accurate representation of the Service Requests and their dependencies.
- You can see the usage in the GraphHelper.cs file in the Helpers folder.
- It allowed me to make a highly customizable and accurate graphical representation of all of the service requests as well as all of their dependencies.
## Heaps
- I struggled to think of where to use the heaps in the ServiceRequests as I already was using an AVL Tree for priority.
- So I added the functionality to the reports system from part 1!
- It allows the user to efficiently order the reports from highest to lowest priority (MaxHeap)
- Or from lowest to highest (MinHeap)
- Priorities are exactly what Heaps are good at and I even added coded to translate the priority value from it's numerical value to it's word equivalent. Just so it is easily digestable for the user.
- 1 = High 2 = Medium 3 = Low

[^1]: GeeksforGeeks. (2018). Complexity of different operations in Binary tree, Binary Search Tree and AVL tree - GeeksforGeeks. [online] Available at: [https://www.geeksforgeeks.org/complexity-different-operations-binary-tree-binary-search-tree-avl-tree/].
