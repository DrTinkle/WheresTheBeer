# WheresTheBeer

Test at https://wheresthebeer.azurewebsites.net/

[View My Presentation]([docs/my_presentation.pdf](https://github.com/DrTinkle/WheresTheBeer/blob/main/WheresTheBeer%20Presentation/Presentation.pdf))

**WheresTheBeer** is a **Blazor WebAssembly** app that helps users find nearby places that serve beer. It integrates with the **Google Places API** to provide live data on bars and restaurants. The app can be installed as a **Progressive Web App (PWA)**, allowing users to add it to their devices for offline access.

## Features

- **Nearby Places Search**: Find bars and restaurants that serve beer near your current location or a specified area.
- **PWA Installation**: Users can install the app on their devices for quick access and offline functionality.
- **Google Places API Integration**: Displays key information such as ratings, reviews, price level, and business status.
- **Responsive Design**: Optimized for both desktop and mobile devices.

## Instructions for Use

As the app is still in development, there are a few steps to follow to ensure proper functionality:

1. **Open the App**: Launch the app via a web browser.
2. **Refresh the Page**: After opening the app, refresh the page to ensure all resources are loaded correctly.
3. **Search for Places**: Once the server is successfully pinged, use the "Find Nearby Places" button to search for bars or restaurants serving beer near your location.

### Known Issues
- Sometimes the app might not respond immediately after being loaded. A page refresh is recommended before searching for places.
- The install button for the PWA may not appear under certain conditions (e.g., if the app is already installed or if using unsupported browsers).

## Key Technologies

- **Blazor WebAssembly**: A front-end framework for building interactive web UIs.
- **Google Places API**: Used to fetch live data for bars and restaurants.
- **Progressive Web App (PWA)**: Allows the app to be installed on mobile devices and accessed offline.
  
## License

This project is licensed under the MIT License.
