# iREACH Anroid App

iReach Android App allows the user to update their profile and record their daily activity with minimum interaction.

Found in the iReachAndroid file is the following:
  - api_interaction_kit
  - iReach_Android
  - iReachAndroid
  
## api_interaction_kit
This is the engine that runs everything behind User Interface.
Inside the api_interaction_kit folder you'll also find the api_interaction_kit_test_suite, which is full of unit tets that test parts of the engine.
### Adding Functionality To The Engine
> In order to create new functionality in the app, the developer only has to follow the following simple steps. First the developer goes into the file json.cs that’s found in the api_interaction_kit and either create a new DataContracted class or find one that they can utilize. It’s important to note here that the class’s member variables must be names the same as the json members that the API is expecting. Next the developer goes into the file api_function_calls.cs and creates the actual functionality that the app will be using. From there the developer creates an event object in the event_object.cs file that stores the required json data and when executed will run the function created in the api_function_calls.cs. These events must contain a reference to the parent who enqueues it, and the execute function must be overridden. Finally the developer heads over to the api.cs file and creates an api_* function that will be the public interface for that functionality. This function will simply enqueue an event into the appropriate queue. Also at this point if it’s a new functionality the developer should also create a new response type enum so the GUI will know what it’s getting back. Now all the developer has to do is link the new functionality to the GUI. This is done by adding a new response type check to the server_response event listener and adding the new api_* function where it is appropriate.

## iReach_Android
This app is what was used to demo at the Senior Design Fair and is the most up to date on how to combine the gui with the engine.

## iReachAndroid
This is the origional app that uses fragments. The UI Designer that left the group left this underdeveloped but if you like the look it is salvageable.

# ToDo
- Link the USDA Food Database to the app and allow the users to input food in an auto complete text field.
- Save user information so they dont have to log in all the time
- Fix the apps ability to auto start when the phone turns on

# Change's to be made / Last Thoughts
> These are just thoughts on how to make the app better
- Change the battery and wifi controls back to the engine from the GUI
-- I had trouble with permisions and didnt notice that the Android Manifest had to be updated until it was pretty late into development.
- Change the State machine to pull from a file instead of being in memory.
- Switch from stacks to a file to hold event objects