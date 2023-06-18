CREATE TABLE Feedbacks (
    feedbackId uuid PRIMARY KEY DEFAULT uuid_generate_v4(),
    userId uuid REFERENCES users (userId),
    eventId uuid REFERENCES events (eventId),
    datetime TIMESTAMP NOT NULL,
    feedback VARCHAR(255) NOT NULL,
    value INTEGER NOT NULL
);