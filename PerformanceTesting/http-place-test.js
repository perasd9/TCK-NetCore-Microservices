import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    vus: 50,
    duration: '30s',
};

export default function () {
    let res = http.get('https://localhost:9501/api/v1/places');
    check(res, {
        'status is 200': (r) => r.status === 200,
    });
    //sleep(0.0001);
}