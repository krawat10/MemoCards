import store from "../store";

const apiRequest = async (url, method, data, anonymous) => {
    let body;
    const {user} = store.getState();
    const headers = {};

    if(data){
        headers["Content-Type"] = "application/json";
    }

    if (user.tokenValue) {
        headers["Authorization"] = `Bearer ${user.tokenValue}`;
    }

    if (data) {
        body = JSON.stringify(data);
    }

    const response = await fetch(url, { method, headers, body});

    if (response.status >= 400) throw new Error(await response.text());

    const contentType = response.headers.get("content-type");

    if(contentType && contentType.indexOf("application/json") !== -1){
        return await response.json();
    }
};

export const get = (url) => apiRequest(url, 'GET');
export const del = (url) => apiRequest(url, 'DELETE');
export const post = (url, data) => apiRequest(url, 'POST', data);
