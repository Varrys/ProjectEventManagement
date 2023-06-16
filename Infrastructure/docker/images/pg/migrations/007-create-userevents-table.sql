CREATE TABLE userEvents
(
    eventId  uuid REFERENCES events (eventId),
    userId   uuid REFERENCES users (userId),
    feedback VARCHAR(255) NOT NULL
);