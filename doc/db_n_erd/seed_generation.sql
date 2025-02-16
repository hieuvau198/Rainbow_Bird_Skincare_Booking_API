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


-- Begins here, Quiz seed data

INSERT INTO [dbo].[Quiz] ([name], [description], [category], [total_points])
VALUES 
(
    'Skin Type Assessment',
    'A comprehensive quiz to determine your basic skin type (oily, dry, combination, or normal) and understand your skin''s natural characteristics.',
    'Skin Type',
    100
),
(
    'Skin Sensitivity Analysis',
    'Evaluate how sensitive your skin is to different factors including environmental conditions, products, and treatments to help prevent adverse reactions.',
    'Sensitivity',
    80
),
(
    'Acne & Breakout Evaluation',
    'Understand your acne patterns, triggers, and severity to help determine the most effective treatment approach for your skin concerns.',
    'Acne',
    90
),
(
    'Anti-Aging Skin Assessment',
    'Assess your skin''s aging concerns and determine which areas need the most attention in your anti-aging skincare routine.',
    'Anti-Aging',
    85
),
(
    'Skin Hydration Check',
    'Evaluate your skin''s moisture levels and barrier function to help optimize your hydration routine and product selection.',
    'Hydration',
    75
);

-- Begins here, Question seed data

-- Questions for Skin Type Assessment (Quiz 1)
INSERT INTO [dbo].[Question] ([quiz_id], [content], [points], [is_multiple_choice], [display_order])
VALUES 
(1, 'How does your skin feel a few hours after washing your face?', 20, 1, 1),
(1, 'Which best describes your pore size?', 20, 1, 2),
(1, 'How often does your skin look shiny throughout the day?', 20, 1, 3),
(1, 'Do you experience tightness or flaking in your skin?', 20, 1, 4),
(1, 'How does your skin react to new products?', 20, 1, 5);

-- Questions for Skin Sensitivity Analysis (Quiz 2)
INSERT INTO [dbo].[Question] ([quiz_id], [content], [points], [is_multiple_choice], [display_order])
VALUES 
(2, 'Does your skin often become red or irritated after using skincare products?', 16, 1, 1),
(2, 'How does your skin react to sun exposure?', 16, 1, 2),
(2, 'Do you experience burning or stinging sensations with certain products?', 16, 1, 3),
(2, 'How does your skin react to temperature changes?', 16, 1, 4),
(2, 'Do you have a history of skin allergies or reactions?', 16, 1, 5);

-- Questions for Acne & Breakout Evaluation (Quiz 3)
INSERT INTO [dbo].[Question] ([quiz_id], [content], [points], [is_multiple_choice], [display_order])
VALUES 
(3, 'How frequently do you experience breakouts?', 18, 1, 1),
(3, 'What type of acne do you typically experience?', 18, 1, 2),
(3, 'In which areas do you most commonly experience breakouts?', 18, 1, 3),
(3, 'Do your breakouts leave marks or scars?', 18, 1, 4),
(3, 'What triggers seem to cause your breakouts?', 18, 1, 5);

-- Questions for Anti-Aging Skin Assessment (Quiz 4)
INSERT INTO [dbo].[Question] ([quiz_id], [content], [points], [is_multiple_choice], [display_order])
VALUES 
(4, 'What are your primary aging concerns?', 17, 1, 1),
(4, 'How visible are fine lines and wrinkles on your face?', 17, 1, 2),
(4, 'How would you describe your skin''s elasticity?', 17, 1, 3),
(4, 'Have you noticed any age spots or hyperpigmentation?', 17, 1, 4),
(4, 'How would you rate your skin''s overall firmness?', 17, 1, 5);

-- Questions for Skin Hydration Check (Quiz 5)
INSERT INTO [dbo].[Question] ([quiz_id], [content], [points], [is_multiple_choice], [display_order])
VALUES 
(5, 'How does your skin feel after cleansing?', 15, 1, 1),
(5, 'Do you experience dry patches on your skin?', 15, 1, 2),
(5, 'How many glasses of water do you drink daily?', 15, 1, 3),
(5, 'Does your skin absorb moisturizer quickly?', 15, 1, 4),
(5, 'How does your skin react to humid vs dry environments?', 15, 1, 5);