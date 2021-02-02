
DECLARE 
	 @ViewFunctionId	VARCHAR(8)
	,@AddEditFunctionId VARCHAR(8)
	,@DeleteFunctionId	VARCHAR(8)
	,@ApproveFuntionId	VARCHAR(8)
	,@AssignFuntionId	VARCHAR(8)
	,@menuName			VARCHAR(50)
	,@ParentGroup		VARCHAR(50)
	,@linkPage			VARCHAR(255)
	,@menuGroup			VARCHAR(50)
	,@Class				VARCHAR(50)
	,@position			INT
	,@isActive			CHAR(1)
	,@groupPosition		INT

--[User Management]
SELECT 
	 @isActive = 1
	,@ParentGroup = 'MIS Report'
	,@Class = 'mdi mdi-file-chart'

BEGIN
	
	select @menuGroup = 'Agent Business Report',@groupPosition = 34
	--1) Agent Business Report
	SELECT
		 @ViewFunctionId	= '112500'
		,@menuName			= 'Agent Business Report'
		,@linkPage			= '/Agent/AgentBusinessReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'
	
	--2) Business Summary Report
	SELECT
		 @ViewFunctionId	= '112505'
		,@menuName			= 'Business Summary Report'
		,@linkPage			= '/Agent/AgentNewBusinessSummaryReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--3) Business Detail Report
	SELECT
		 @ViewFunctionId	= '112510'
		,@menuName			= 'Business Detail Report'
		,@linkPage			= '/Agent/AgentNewBusinessDetailReport'
		,@position = 3
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


		--4) Passive Agent Report
		SELECT
		@ViewFunctionId	= '112515'
		,@menuName			= 'Passive Agent Report'
		,@linkPage			= '/Agent/PassiveAgentReport'
		,@position = 4
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


	select @menuGroup = 'Business Report',@groupPosition = 35
	--1) Renewal Business Report
	SELECT
		 @ViewFunctionId	= '112600'
		,@menuName			= 'Renewal Business Report'
		,@linkPage			= '/DynamicReport/RenewalBusinessReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Renewal Business Summary Report
	SELECT
		 @ViewFunctionId	= '112605'
		,@menuName			= 'Renewal Business Summary Report'
		,@linkPage			= '/DynamicReport/RenewalBusinessSummaryReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--3) New Business Summary Report
	SELECT
		 @ViewFunctionId	= '112610'
		,@menuName			= 'New Business Summary Report'
		,@linkPage			= '/DynamicReport/NewBusinessSummaryReport'
		,@position = 3
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'



		--4) AML report
	SELECT
		 @ViewFunctionId	= '112615'
		,@menuName			= 'AML Report'
		,@linkPage			= '/DynamicReport/AMLReport'
		,@position = 4
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


	select @menuGroup = 'Policy Loan Detail',@groupPosition = 36
	--1) Forfeiture Report
	SELECT
		 @ViewFunctionId	= '112700'
		,@menuName			= 'Forfeiture Report'
		,@linkPage			= '/PolicyLoanDetail/ForfeitureReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Policy Loan Report
	SELECT
		 @ViewFunctionId	= '112705'
		,@menuName			= 'Policy Loan Report'
		,@linkPage			= '/PolicyLoanDetail/PolicyLoanReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--3) Policy Loan Summary Report
	SELECT
		 @ViewFunctionId	= '112710'
		,@menuName			= 'Policy Loan Summary Report'
		,@linkPage			= '/PolicyLoanDetail/PolicyLoanSummaryReport'
		,@position = 3
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--4) Policy Loan Repayment Report
	SELECT
		 @ViewFunctionId	= '112715'
		,@menuName			= 'Policy Loan Repayment Report'
		,@linkPage			= '/PolicyLoanDetail/PolicyLoanRepaymentReport'
		,@position = 4
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Surrender',@groupPosition = 37
	--1) Surrender Summary Report
	SELECT
		 @ViewFunctionId	= '112800'
		,@menuName			= 'Surrender Summary Report'
		,@linkPage			= '/Surrender/SurrenderSummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Surrender Detail Report
	SELECT
		 @ViewFunctionId	= '112810'
		,@menuName			= 'Surrender Detail Report'
		,@linkPage			= '/Surrender/SurrenderDetailReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Survival Benefits',@groupPosition = 38
	--1) Summary Report
	SELECT
		 @ViewFunctionId	= '112900'
		,@menuName			= 'Summary Report'
		,@linkPage			= '/SurvivalBenefits/SurvivalBenefitsSummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Survival Benefits Detail Report
	SELECT
		 @ViewFunctionId	= '112910'
		,@menuName			= 'Detail Report'
		,@linkPage			= '/SurvivalBenefits/SurvivalBenefitsDetailReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Renewal Premium',@groupPosition = 39
	--1) Summary Report
	SELECT
		 @ViewFunctionId	= '113000'
		,@menuName			= 'Summary Report'
		,@linkPage			= '/RenewalPremium/PremiumSummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


		--2) Renewal Dates Report
	SELECT
		 @ViewFunctionId	= '113010'
		,@menuName			= 'Renewal Dates Report'
		,@linkPage			= '/RenewalPremium/RenewalDates'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Maturity',@groupPosition = 40
	--1) Summary Report
	SELECT
		 @ViewFunctionId	= '113100'
		,@menuName			= 'Summary Report'
		,@linkPage			= '/Maturity/MaturitySummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Detail Report
	SELECT
		 @ViewFunctionId	= '113110'
		,@menuName			= 'Detail Report'
		,@linkPage			= '/Maturity/MaturityDetailReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Lapsed',@groupPosition = 41
	--1) Summary Report
	SELECT
		 @ViewFunctionId	= '113200'
		,@menuName			= 'Summary Report'
		,@linkPage			= '/PolicyDetail/LapsedSummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Lapsed Detail Report
	SELECT
		 @ViewFunctionId	= '113205'
		,@menuName			= 'Detail Report'
		,@linkPage			= '/PolicyDetail/LapsedDetailReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--3) Lapsed Revival Summary Report
	SELECT
		 @ViewFunctionId	= '113210'
		,@menuName			= 'Revival Summary Report'
		,@linkPage			= '/PolicyDetail/LapsedRevivalSummaryReport'
		,@position = 3
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--4) Lapsed Revival Summary Report
	SELECT
		 @ViewFunctionId	= '113220'
		,@menuName			= 'Revival Detail Report'
		,@linkPage			= '/PolicyDetail/LapsedRevivalDetailReport'
		,@position = 4
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Death Claim Report',@groupPosition = 41
	--1) Summary Report
	SELECT
		 @ViewFunctionId	= '113300'
		,@menuName			= 'Summary Report'
		,@linkPage			= '/DeathClaim/DeathClaimSummaryReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	--2) Claim Detail Report
	SELECT
		 @ViewFunctionId	= '113310'
		,@menuName			= 'Detail Report'
		,@linkPage			= '/DeathClaim/DeathClaimDetailReport'
		,@position = 2
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

	select @menuGroup = 'Proposal',@groupPosition = 42
	--2) Proposal
	SELECT
		 @ViewFunctionId	= '113500'
		,@menuName			= 'Proposal Report'
		,@linkPage			= '/DynamicReport/ProposalReport'
		,@position = 1
		
		EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

		
		select @menuGroup = 'PlanWiseBusiness',@groupPosition = 43
 --2) planwise BUssinessreport
		 SELECT
		   @ViewFunctionId = '113600'
		  ,@menuName   = 'PlanWise Business Report'
		  ,@linkPage   = '/DynamicReport/PlanWiseReport'
		  ,@position = 1
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

END


		select @menuGroup = 'GroupPolicyReport',@groupPosition = 44
--2) Group Policy Report
		 SELECT
		   @ViewFunctionId = '113700'
		  ,@menuName   = 'Group Policy Report'
		  ,@linkPage   = '/DynamicReport/GroupPolicyReport'
		  ,@position = 1
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


		  
		select @menuGroup = 'GroupPolicyReport',@groupPosition = 45
--2) Group Policy Report
		 SELECT
		   @ViewFunctionId = '113800'
		  ,@menuName   = 'Group Policy Detail Report'
		  ,@linkPage   = '/GroupPolicy/GroupPolicyReport'
		  ,@position = 2
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'


		    
		select @menuGroup = 'GroupPolicyReport',@groupPosition = 48
--2) Group Policy Report
		 SELECT
		   --@ViewFunctionId = '113800'
		  @menuName   = 'Group Policy New Detail Report'
		  ,@linkPage   = '/Dynamic/GroupPolicySummaryNewReport'
		  ,@position = 3
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'
		


		  select @menuGroup = 'CurrentDayReport',@groupPosition = 46
		    --2) Current Day Report
		 SELECT
		   @ViewFunctionId = '113900'
		  ,@menuName   = 'Current Day Report'
		  ,@linkPage   = '/DynamicReport/CurrentDayReport'
		  ,@position = 1
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'



		   select @menuGroup = 'Regional Report',@groupPosition = 47
--1) AgentNewBusinessSummaryRegionalReport
		 SELECT
		   @ViewFunctionId = '114000'
		  ,@menuName   = 'AgentNewBusinessSummaryRegionalReport'
		  ,@linkPage   = '/RegionalReports/AgentNewBusinessSummaryRegionalReport'
		  ,@position = 1
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

 --2) AgentNewBusinessDetailRegionalReport
		 SELECT
		   @ViewFunctionId = '114100'
		  ,@menuName   = 'AgentNewBusinessDetailRegionalReport'
		  ,@linkPage   = '/RegionalReports/AgentNewBusinessDetailRegionalReport'
		  ,@position = 2
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'

--2) AgentNewBusinessDetailRegionalReport
		 SELECT
		   @ViewFunctionId = '114200'
		  ,@menuName   = 'RegionalAsOnReport'
		  ,@linkPage   = '/RegionalReports/RegionalAsOnReport'
		  ,@position = 3
  
		  EXEC proc_addMenu  @ViewFunctionId, @menuName, @linkPage, @menuGroup, @position, @isActive, @groupPosition,@ParentGroup,@Class
		  EXEC proc_AddFunction @ViewFunctionId, @ViewFunctionId, 'View'






		