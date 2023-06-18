CREATE TABLE Activities (
    activityId uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(255) NOT NULL,
    datetime TIMESTAMP NOT NULL,
    description TEXT,
    eventId uuid REFERENCES events (eventId)
);