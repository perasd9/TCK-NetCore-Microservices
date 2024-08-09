import grpc from "k6/net/grpc";
import { check } from "k6";

export let options = {
    vus: 50,
    duration: "30s",
};

const client = new grpc.Client();
client.load(["."], "../TypesOfSportingEvents.API/Core/Protos/typeOfSportingEvent.proto");
let i = 0;

export default function () {
    if (i == 0) {
        i++;
        client.connect("localhost:9201", {});
    }

  let response = client.invoke("gRPCTypeOfSportingEventService/GetById", {"id":"6a85c3dc-6ac5-4b18-bbfd-60303c5c8967"});

    check(response, {
        "status is OK": (r) => r && r.status === grpc.StatusOK,
    });
}
