import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  vus: 30,
  duration: '30s',
};

export default function () {
  let res = http.get('https://localhost:9201/api/v1/types-of-sporting-events');
  check(res, {
    'status is 200': (r) => r.status === 200,
  });

}