import axios from 'axios';

const getAccessTokenConfig = async(getAccessTokenSilently) => {



    const accessToken = await getAccessTokenSilently({
        audience: `${process.env.REACT_APP_API_AUDIENCE}`,
        scope: "read:notes"
    });

    let config = {
        headers: {
            'Authorization': `Bearer ${accessToken}`
        }
    }

    return config;
};

const getData = async(url, getAccessTokenSilently) => {


    const urlFull = `${process.env.REACT_APP_API_BASE_URL}${url}`;


    if (getAccessTokenSilently) {
        const config = await getAccessTokenConfig(getAccessTokenSilently);
        const result = await axios.get(urlFull, config);
        return result.data;
    } else {
        const result = await axios.get(urlFull);
        return result.data;
    }

};

const postData = async(url, data, getAccessTokenSilently) => {


    const urlFull = `${process.env.REACT_APP_API_BASE_URL}${url}`;

    if (getAccessTokenSilently) {
        const config = await getAccessTokenConfig(getAccessTokenSilently);
        const result = await axios.post(urlFull, data, config);
    } else {
        const result = await axios.post(urlFull, data);
    }

}

export { getData, postData };