import store from "../store";

const apiRequest = async (url, method, data, anonymous) => {
    let body;
    const {user} = store.getState();
    const headers = {};

    if(data){
        headers["Content-Type"] = "application/json";
    }

    // token is stored in application store (Redux) and local storage
    if (user.tokenValue) {
        headers["Authorization"] = `Bearer ${user.tokenValue}`; // Authorization token is added to request header
    }

    if (data) {
        body = JSON.stringify(data);
    }

    const response = await fetch(url, { method, headers, body}); // HTTP request

    if (response.status >= 400) throw new Error(await response.text());

    const contentType = response.headers.get("content-type");

    if(contentType && contentType.indexOf("application/json") !== -1){
        return await response.json();
    }

    if(contentType && contentType.indexOf("text/xml") !== -1){
        const parseString = require('xml2js').parseStringPromise;
        const text = await response.text();
        return await parseString(text);
    }
};

export const get = (url) => apiRequest(url, 'GET');
export const del = (url) => apiRequest(url, 'DELETE');
export const post = (url, data) => apiRequest(url, 'POST', data);
export const put = (url, data) => apiRequest(url, 'PUT', data);
