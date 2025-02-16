USE prestinedb

BEGIN -- [DONE] Working Day data, Monday to Sartuday
	INSERT INTO [dbo].[WorkingDay] (day_name, start_time, end_time, slot_duration_minutes, is_active)
	VALUES 
	('Monday', '08:00:00', '17:00:00', 30, 1),
	('Tuesday', '08:00:00', '17:00:00', 30, 1),
	('Wednesday', '08:00:00', '17:00:00', 30, 1),
	('Thursday', '08:00:00', '17:00:00', 30, 1),
	('Friday', '08:00:00', '17:00:00', 30, 1),
	('Saturday', '08:00:00', '17:00:00', 30, 1);
END

BEGIN -- [DONE] Time Slot data, need Working Day first
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

END

BEGIN -- [DONE] Quiz data, 5 quiz collection
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

END

BEGIN -- [DONE] Question data, 5 question per quiz, total of 25
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
END

BEGIN -- [DONE] Answer data, 3-4 answer per question, total of 86
	-- Answers for Skin Type Assessment (Quiz 1)
	-- Question 1: How does your skin feel a few hours after washing your face?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(1, 'Very oily all over', 20),
	(1, 'Somewhat oily in T-zone only', 15),
	(1, 'Normal and balanced', 10),
	(1, 'Dry and tight', 5);

	-- Question 2: Which best describes your pore size?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(2, 'Large and visible', 20),
	(2, 'Medium-sized, visible in T-zone', 15),
	(2, 'Small and barely visible', 10);

	-- Question 3: How often does your skin look shiny throughout the day?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(3, 'Always shiny after a few hours', 20),
	(3, 'Sometimes shiny in T-zone', 15),
	(3, 'Rarely shiny', 10),
	(3, 'Never shiny', 5);

	-- Question 4: Do you experience tightness or flaking in your skin?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(4, 'Never', 20),
	(4, 'Occasionally', 15),
	(4, 'Frequently', 10);

	-- Question 5: How does your skin react to new products?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(5, 'Often breaks out', 20),
	(5, 'Sometimes becomes irritated', 15),
	(5, 'Usually adapts well', 10);

	-- Answers for Skin Sensitivity Analysis (Quiz 2)
	-- Question 6: Does your skin often become red or irritated after using skincare products?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(6, 'Yes, frequently', 16),
	(6, 'Sometimes', 12),
	(6, 'Rarely', 8),
	(6, 'Never', 4);

	-- Question 7: How does your skin react to sun exposure?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(7, 'Burns easily', 16),
	(7, 'Sometimes burns, then tans', 12),
	(7, 'Rarely burns, tans well', 8);

	-- Question 8: Do you experience burning or stinging sensations with certain products?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(8, 'Often', 16),
	(8, 'Sometimes', 12),
	(8, 'Rarely or never', 8);

	-- Question 9: How does your skin react to temperature changes?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(9, 'Becomes red and irritated', 16),
	(9, 'Slightly sensitive', 12),
	(9, 'No noticeable reaction', 8);

	-- Question 10: Do you have a history of skin allergies or reactions?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(10, 'Yes, frequent allergic reactions', 16),
	(10, 'Occasional reactions', 12),
	(10, 'No history of reactions', 8);

	-- Answers for Acne & Breakout Evaluation (Quiz 3)
	-- Question 11: How frequently do you experience breakouts?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(11, 'Daily', 18),
	(11, 'Weekly', 14),
	(11, 'Monthly', 10),
	(11, 'Rarely', 6);

	-- Question 12: What type of acne do you typically experience?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(12, 'Cystic acne', 18),
	(12, 'Whiteheads and blackheads', 14),
	(12, 'Occasional small pimples', 10);

	-- Question 13: In which areas do you most commonly experience breakouts?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(13, 'All over face', 18),
	(13, 'T-zone only', 14),
	(13, 'Chin and jawline', 10);

	-- Question 14: Do your breakouts leave marks or scars?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(14, 'Yes, often', 18),
	(14, 'Sometimes', 14),
	(14, 'Rarely or never', 10);

	-- Question 15: What triggers seem to cause your breakouts?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(15, 'Stress and hormones', 18),
	(15, 'Diet and lifestyle', 14),
	(15, 'Skincare products', 10),
	(15, 'Not sure', 6);

	-- Answers for Anti-Aging Skin Assessment (Quiz 4)
	-- Question 16: What are your primary aging concerns?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(16, 'Fine lines and wrinkles', 17),
	(16, 'Loss of firmness and elasticity', 15),
	(16, 'Uneven skin tone and age spots', 13),
	(16, 'No major concerns yet', 10);

	-- Question 17: How visible are fine lines and wrinkles on your face?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(17, 'Very visible, deep wrinkles', 17),
	(17, 'Moderately visible', 14),
	(17, 'Barely visible', 11);

	-- Question 18: How would you describe your skin''s elasticity?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(18, 'Significantly decreased', 17),
	(18, 'Somewhat decreased', 14),
	(18, 'Still maintains good elasticity', 11),
	(18, 'Excellent elasticity', 8);

	-- Question 19: Have you noticed any age spots or hyperpigmentation?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(19, 'Yes, multiple spots', 17),
	(19, 'A few spots starting to appear', 14),
	(19, 'No visible spots', 11);

	-- Question 20: How would you rate your skin''s overall firmness?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(20, 'Significant loss of firmness', 17),
	(20, 'Moderate loss of firmness', 14),
	(20, 'Minimal loss of firmness', 11),
	(20, 'Very firm', 8);

	-- Answers for Skin Hydration Check (Quiz 5)
	-- Question 21: How does your skin feel after cleansing?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(21, 'Very tight and dry', 15),
	(21, 'Slightly tight', 12),
	(21, 'Comfortable and balanced', 9),
	(21, 'No noticeable change', 6);

	-- Question 22: Do you experience dry patches on your skin?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(22, 'Frequently', 15),
	(22, 'Occasionally', 12),
	(22, 'Rarely', 9);

	-- Question 23: How many glasses of water do you drink daily?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(23, 'Less than 4 glasses', 15),
	(23, '4-6 glasses', 12),
	(23, '7-8 glasses', 9),
	(23, 'More than 8 glasses', 6);

	-- Question 24: Does your skin absorb moisturizer quickly?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(24, 'Yes, very quickly', 15),
	(24, 'At a normal rate', 12),
	(24, 'Takes time to absorb', 9);

	-- Question 25: How does your skin react to humid vs dry environments?
	INSERT INTO [dbo].[Answer] ([question_id], [content], [points]) VALUES 
	(25, 'Becomes very dry in dry environments', 15),
	(25, 'Slightly affected by environment changes', 12),
	(25, 'Maintains balance in both conditions', 9),
	(25, 'No noticeable difference', 6);
END

BEGIN -- [System needs Category for Service]Service data
	INSERT INTO [dbo].[Service] (
		[service_name],
		[description],
		[price],
		[duration_minutes],
		[location],
		[service_image]
	) VALUES 
	(
		'Deep Cleansing Facial',
		'A thorough facial treatment that includes double cleansing, exfoliation, extraction, and hydration. Perfect for removing impurities and restoring skin balance.',
		89.99,
		60,
		'Treatment Room 1',
		'/images/services/deep-cleansing-facial.jpg'
	),
	(
		'Advanced Hydration Therapy',
		'Intensive hydrating treatment using hyaluronic acid and moisture-binding ingredients to restore skin''s moisture barrier and improve plumpness.',
		129.99,
		75,
		'Treatment Room 2',
		'/images/services/hydration-therapy.jpg'
	),
	(
		'Anti-Aging LED Light Therapy',
		'Advanced LED light therapy session targeting fine lines, wrinkles, and promoting collagen production. Includes a gentle cleanse and finishing moisturizer.',
		149.99,
		45,
		'LED Treatment Room',
		'/images/services/led-therapy.jpg'
	),
	(
		'Acne Control Treatment',
		'Specialized treatment for acne-prone skin including deep cleansing, targeted extractions, and anti-bacterial light therapy. Includes post-care instructions.',
		109.99,
		90,
		'Treatment Room 3',
		'/images/services/acne-treatment.jpg'
	),
	(
		'Microdermabrasion Session',
		'Professional exfoliation treatment that removes dead skin cells and promotes cell renewal. Includes a soothing mask and SPF application.',
		119.99,
		60,
		'Treatment Room 1',
		'/images/services/microdermabrasion.jpg'
	),
	(
		'Chemical Peel Treatment',
		'Customized chemical peel to address specific skin concerns such as hyperpigmentation, acne scars, or fine lines. Includes post-peel care kit.',
		159.99,
		45,
		'Treatment Room 2',
		'/images/services/chemical-peel.jpg'
	),
	(
		'Oxygen Facial Therapy',
		'Refreshing oxygen infusion treatment that delivers vitamins and nutrients deep into the skin. Perfect for dull, tired skin.',
		139.99,
		60,
		'Treatment Room 3',
		'/images/services/oxygen-facial.jpg'
	),
	(
		'Sensitive Skin Calming Treatment',
		'Gentle treatment designed for sensitive or rosacea-prone skin. Uses calming ingredients and includes LED red light therapy.',
		99.99,
		60,
		'Treatment Room 1',
		'/images/services/sensitive-skin-treatment.jpg'
	),
	(
		'Ultrasonic Skin Scrubbing',
		'Deep cleansing treatment using ultrasonic waves to remove impurities and dead skin cells. Includes hydrating mask.',
		119.99,
		45,
		'Treatment Room 2',
		'/images/services/ultrasonic-treatment.jpg'
	),
	(
		'Anti-Aging Collagen Boost',
		'Intensive anti-aging treatment including collagen-stimulating massage, peptide serums, and firming mask.',
		179.99,
		90,
		'Treatment Room 3',
		'/images/services/collagen-boost.jpg'
	),
	(
		'Express Refresh Facial',
		'Quick but effective facial treatment including cleansing, exfoliation, and hydration. Perfect for lunch breaks.',
		69.99,
		30,
		'Treatment Room 1',
		'/images/services/express-facial.jpg'
	),
	(
		'Brightening Vitamin C Treatment',
		'Powerful brightening treatment using concentrated Vitamin C and other antioxidants. Includes gentle exfoliation and brightening mask.',
		129.99,
		60,
		'Treatment Room 2',
		'/images/services/vitamin-c-treatment.jpg'
	),
	(
		'Detox Clay Therapy',
		'Deep cleansing treatment using therapeutic clays to draw out impurities. Includes lymphatic drainage massage.',
		99.99,
		75,
		'Treatment Room 3',
		'/images/services/clay-therapy.jpg'
	),
	(
		'Hydro-Jelly Mask Treatment',
		'Cooling and hydrating treatment using advanced hydro-jelly mask technology. Perfect for dehydrated or sun-damaged skin.',
		109.99,
		45,
		'Treatment Room 1',
		'/images/services/hydro-jelly-mask.jpg'
	),
	(
		'Premium Facial Package',
		'Comprehensive facial treatment including cleansing, exfoliation, massage, custom mask, and LED therapy. Our most luxurious service.',
		199.99,
		120,
		'VIP Treatment Room',
		'/images/services/premium-facial.jpg'
	);
END