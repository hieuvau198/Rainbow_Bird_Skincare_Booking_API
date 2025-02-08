-- Begins here -- Insert Working Days (Monday to Saturday)
INSERT INTO [dbo].[WorkingDay] (day_name, start_time, end_time, slot_duration_minutes, is_active)
VALUES 
('Monday', '08:00:00', '17:00:00', 30, 1),
('Tuesday', '08:00:00', '17:00:00', 30, 1),
('Wednesday', '08:00:00', '17:00:00', 30, 1),
('Thursday', '08:00:00', '17:00:00', 30, 1),
('Friday', '08:00:00', '17:00:00', 30, 1),
('Saturday', '08:00:00', '17:00:00', 30, 1);



-- Begins here -- Generate Time Slots for Each Working Day
DECLARE @workingDayId INT, @dayName NVARCHAR(20)
DECLARE @startTime TIME(7) = '08:00:00'
DECLARE @endTime TIME(7) = '17:00:00'
DECLARE @slotDuration INT = 30 -- Slot duration in minutes

DECLARE workingDayCursor CURSOR FOR 
SELECT working_day_id, day_name FROM [dbo].[WorkingDay];

OPEN workingDayCursor;
FETCH NEXT FROM workingDayCursor INTO @workingDayId, @dayName;

WHILE @@FETCH_STATUS = 0
BEGIN
    DECLARE @currentTime TIME(7) = @startTime
    DECLARE @slotNumber INT = 1

    WHILE @currentTime < @endTime
    BEGIN
        DECLARE @nextTime TIME(7) = DATEADD(MINUTE, @slotDuration, @currentTime)

        -- Skip slots between 11:30 AM and 1:00 PM
        IF @currentTime >= '11:30:00' AND @currentTime < '13:00:00'
        BEGIN
            SET @currentTime = @nextTime
            CONTINUE
        END

        -- Insert the time slot for the current working day
        INSERT INTO [dbo].[TimeSlot] (working_day_id, start_time, end_time, slot_number, is_active)
        VALUES (@workingDayId, @currentTime, @nextTime, @slotNumber, 1);

        -- Move to the next time slot
        SET @currentTime = @nextTime
        SET @slotNumber = @slotNumber + 1
    END

    -- Fetch the next working day
    FETCH NEXT FROM workingDayCursor INTO @workingDayId, @dayName;
END

CLOSE workingDayCursor;
DEALLOCATE workingDayCursor;