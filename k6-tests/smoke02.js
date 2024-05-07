import http from 'k6/http';
import { sleep } from 'k6';
import * as config from './config.js';

// Define your API key
const apiKey = 'key1';

export const options = {
    vus: 1,
    duration: '10s',

    thresholds: {
        http_req_duration: ['p(95)<1000']
    },
};

export default function () {
    // Send a GET request to the URL with the API key in the headers
    http.get(config.API_REVERSE_URL_DRINKS, {
        headers: {
            'Api-Key': apiKey
        }
    });
    sleep(1);
}