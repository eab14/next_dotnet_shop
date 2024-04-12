import useSWR from 'swr';

const base = `http://localhost:5245/api/`;

const fetcher = async (url) => {

    const response = await fetch(url);

    if (!response.ok) throw new Error('Error fetching...');
    return response.json();

}

export const useRequest = (path, subPath) => {

    if (!path) throw new Error('Path is required')

    const url = subPath ? base + path + '/' + subPath : base + path;
    const { data, error } = useSWR(url, fetcher);

    return { data, error }

}