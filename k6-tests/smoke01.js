// Define an array of URLs to smoke test
const urls = [
  { url: "http://localhost:5000/drinks", expectedStatusCode: 200 },
  { url: "http://localhost:5000/", expectedStatusCode: 200 }
  // Add more URLs as needed
];


import http from 'k6/http';
import { check } from 'k6';

// Define your API key
const apiKey = 'key2';

export default function () {
  urls.forEach(({ url, expectedStatusCode }) => {
      // Send a GET request to the URL with the API key in the headers
      let response = http.get(url, {
          headers: {
              'Api-Key': apiKey
          }
      });

      // Verify the response status code
      check(response, {
          [`Status is ${expectedStatusCode} for ${url}`]: (r) => r.status === expectedStatusCode,
      });
  });
}
