import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
  vus: 50,
  duration: '30s',
};

export default function () {
  let res = http.get('https://localhost:9201/api/v1/types-of-sporting-events/6a85c3dc-6ac5-4b18-bbfd-60303c5c8967');
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
  //sleep(0.0001);
}