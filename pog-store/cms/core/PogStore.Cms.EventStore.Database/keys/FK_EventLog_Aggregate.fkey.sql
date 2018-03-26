ALTER TABLE [dbo].[EventLog]
    ADD CONSTRAINT [FK_Event_EventSource] FOREIGN KEY ([EventSourceId]) REFERENCES [dbo].[EventSource] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION;
