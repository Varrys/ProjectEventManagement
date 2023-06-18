CREATE TABLE userActivities
(
    eventId  uuid REFERENCES events (eventId),
    activityId   uuid REFERENCES activities (activityId)   
);