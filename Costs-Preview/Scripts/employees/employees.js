var CostsPreview = CostsPreview || {

	// load all data
    load: function () {
		$.ajax({
			type: 'GET',
			url: '/Costs/GetData',
			dataType: 'json'
		})
			.done(function (data, textStatus, jqXHR) {
				if (data) {
					CostsPreview.refresh(data);
				}
			})
			.fail(function (jqXHR, textStatus, errorThrown) {
				alert('fail');
			});
	},
	// updates DOM with data
	refresh: function (data) {
		$('#total-costs').html(data.TotalCosts);
		$('#salary-costs').html(data.TotalCostsAnnualSalaries);
		$('#benefits-costs').html(data.TotalCostsAnnualBenefits);
		$('#total-employees').html(data.TotalEmployees);
		$('#total-cost-employees-benefits').html(data.TotalCostsAnnualBenefitsEmployees);
		$('#total-discounts-employees-benefits').html(data.TotalDiscountsEmployees);
		$('#total-dependents').html(data.TotalDependents);
		$('#total-cost-dependents-benefits').html(data.TotalCostsAnnualBenefitsDependents);
		$('#total-discounts-dependents-benefits').html(data.TotalDiscountsDependents);

		if (data.TotalEmployees === 0) {
			$('#no-employee-alert').removeClass('hide');
		} else {
			$('#no-employee-alert').addClass('hide');
		}
		var $emplList = $('div[name=employee-list] div.list-group');
		$emplList.empty();

		for (var i = 0, len = data.Employees.length; i < len; i += 1) {
			$('<a class="list-group-item" data-id="@employee.Id" href="javascript: void(0)">@employee.FirstName @employee.LastName</a>')
			var rec = data.Employees[i];
			$emplList.append($('<a />', {
				'class': 'list-group-item',
				'data-id': rec.Id,
				'href': 'javascript: void(0)',
				'html': rec.FirstName + ' ' + rec.LastName
			}));
		}

		CostsPreview.setEvents();
	},
	// gets a single employee to display 
	getEmployee: function (id) {
		$.ajax({
			type: 'GET',
			url: '/Costs/GetEmployee',
			data: { id: id },
			dataType: 'json'
		})
			.done(function (data, textStatus, jqXHR) {
				if (data) {
					CostsPreview.clearAddEmployeeForm();
					CostsPreview.showAddEmployeeForm();
					$('#add-employee, #add-dependent').addClass('hide');
					$('#add-employee-form').attr('data-id', data.Id);
					$('#employee-first-name').val(data.FirstName);
					$('#employee-last-name').val(data.LastName);
				}
				if (data.Dependents.length) {
					for (var i = 0, len = data.Dependents.length; i < len; i += 1) {
						var template = $('#dependent-form-template').clone().html();
						var $template = $(template);
						$('input[placeholder="First Name"]', $template).val(data.Dependents[i].FirstName);
						$('input[placeholder="Last Name"]', $template).val(data.Dependents[i].LastName);
						$('div[name=dependent-detail]').append($template);
						$('button[name=remove-dependent]', $template).remove();
					}
				}
			})
			.fail(function (jqXHR, textStatus, errorThrown) {
				alert('fail');
			});
	},
	// save employee and dependent information
	saveEmployee: function (employee) {
		$.ajax({
			type: 'POST',
			url: '/Costs/AddEmployee',
			data: { employee: employee },
			dataType: 'json'
		})
			.done(function (data, textStatus, jqXHR) {
				if (data) {
					CostsPreview.refresh(data);
				}
			})
			.fail(function (jqXHR, textStatus, errorThrown) {
				alert('fail');
			})
			.always(function () {
				CostsPreview.hideAddEmployeeForm();
			});
	},
	//remove employee from database
	deleteEmployee: function (id) {
		$.ajax({
			type: 'GET',
			url: '/Costs/DeleteEmployee',
			data: { id: id },
			dataType: 'json'
		})
			.done(function (data, textStatus, jqXHR) {
				if (data) {
					CostsPreview.refresh(data);
				}
			})
			.fail(function (jqXHR, textStatus, errorThrown) {
				alert('fail');
			})
			.always(function () {
				CostsPreview.hideAddEmployeeForm();
			});
	},
	// verifies all inputs have data in them.
	validateForm: function () {
		$('#add-employee-form .has-error').removeClass('has-error');

		if (!$('#employee-first-name').val()) {
			$('#employee-first-name').closest('.form-group').addClass('has-error');
			return false;
		}
		if (!$('#employee-last-name').val()) {
			$('#employee-last-name').closest('.form-group').addClass('has-error');
			return false;
		}

		var $dep = $('div[name=dependent-detail] input');

		$dep.each(function () {
			var $this = $(this);
			if (!$this.val()) {
				$this.closest('.input-group').addClass('has-error');
				return false;
			}
		});

		if ($('#add-employee-form .has-error').length) return false;

		return true;
	},
	// creates the EmployeeModel for uploading
	createEmployeeObjectFromForm: function () {
		var employee = {
			FirstName: $('#employee-first-name').val(),
			LastName: $('#employee-last-name').val(),
			PayPerPeriod: 2000,
			Dependents: []
		};
		var $depRows = $('div[name=dependent-detail] .row');
		$depRows.each(function () {
			var $this = $(this);
			employee.Dependents.push({
				FirstName: $('input[placeholder="First Name"]', $this).val(),
				LastName: $('input[placeholder="Last Name"]', $this).val()
			});
		});
		return employee;
	},
	// update events that change on data load
	setEvents: function () {
		var $emplList = $('div[name=employee-list] div.list-group a');

		$emplList.on('click', function () {
			var $this = $(this);
			var id = $this.attr('data-id');
			CostsPreview.getEmployee(id);
		});
	},
	clearAddEmployeeForm: function () {
		$('#add-employee-form').attr('data-id', '');
		$('#employee-first-name').val('');
		$('#employee-last-name').val('');
		$('div[name=dependent-detail]').empty();
	},
	showAddEmployeeForm: function () {
		$('#add-employee-form').removeClass('hide');
		$('#open-add-employee-dialog').addClass('hide');
		$('#add-employee, #del-employee, #add-dependent').removeClass('hide');
	},
	hideAddEmployeeForm: function () {
		$('#add-employee-form').addClass('hide');
		$('#open-add-employee-dialog').removeClass('hide');
	}
};

$(document).ready(function () {
	$('#costs-overview-nav').on('click', function () {
		$('#Employees').addClass('hide');
		$('#CostsOverview').removeClass('hide');
	});

	$('#employees-nav').on('click', function () {
		$('#CostsOverview').addClass('hide');
		$('#Employees').removeClass('hide');
	});

	$('#open-add-employee-dialog').on('click', function () {
		CostsPreview.showAddEmployeeForm();
		CostsPreview.clearAddEmployeeForm();
		$('#del-employee').addClass('hide');
	});

	$('#add-employee').on('click', function () {
		// verify
		var validated = CostsPreview.validateForm();

		if (!validated) return;

		//create model
		var employee = CostsPreview.createEmployeeObjectFromForm();

		// add model
		CostsPreview.saveEmployee(employee);
	});
	$('#add-dependent').on('click', function () {
		var template = $('#dependent-form-template').clone().html();
		var $template = $(template);
		$('div[name=dependent-detail]').append($template);
		$('button[name=remove-dependent]', $template).on('click', function () {
			$template.remove();
		});
	});
	$('#del-employee').on('click', function () {
		var id = $('#add-employee-form').attr('data-id');
		CostsPreview.deleteEmployee(id);
	});
	$('#cancel-add').on('click', function () {
		CostsPreview.clearAddEmployeeForm();
		CostsPreview.hideAddEmployeeForm();
	});

	CostsPreview.setEvents();
});

