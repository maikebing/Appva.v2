mcss.validation = {
    PreparedSequenceCreate: function (params) {
        $('.std-form form').validate({
            'rules': {
                'Name': 'required'
            },
            'messages': {
                'Name': {
                    'required': "Namn måste fyllas i."
                }
            },
            'submitHandler': function (form) {
                $(form).find('input[type=submit]').attr('disabled', 'disabled');
                form.submit();
            }
        });
    },
	PatientCreate : function(params) {
		var id = params['id'];
        var uidUrl = params['uidUrl'], 
            taxonUrl = params['taxonUrl'];
        $('.lb-panel form').validate({
            'rules': {
                'FirstName': 'required',
                'LastName': 'required',
                'PersonalIdentityNumber': {
                    'required': true,
                    'socialsecuritynumber': 'socialsecuritynumber',
                    'remote': {
                        'url': uidUrl,
                        'type': "post",
                        'data': {
                            'id': id, 
                            'uniqueIdentifier': $('#PersonalIdentityNumber').val()
                        }
                    }
                },
                'Taxon': {
                    'required': true,
                    'remote': {
                        'url': taxonUrl,
                        'type': "post",
                        'data': {
                            'taxon': $('#Taxon').val()
                        }
                    }
                }
            },
            'messages': {
                'FirstName': "Förnamn måste fyllas i.",
                'LastName': "Efternamn måste fyllas i.",
                'PersonalIdentityNumber': {
                    'required': "Personnummer måste fyllas i.",
                    'remote': "Personnumret finns redan tidigare redan i MCSS.",
                    'socialsecuritynumber': "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001."
                },
                'Taxon': "Adress måste väljas."
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	ScheduleCreate : function(params) {
        var id = params['id'];
        var url = params['url'];
        $('.std-form form').validate({
            'rules': {
                'ScheduleSetting': {
                    'required': true,
                    'remote': {
                        'url': url,
                        'type': "post",
                        'data': {
                            'Id': id,
                            'ScheduleSettingId': $('#ScheduleSetting').val()
                        }
                    }
                }
            },
            'messages': {
                'ScheduleSetting': {
                    'required': "Typ av lista måste fyllas i.",
                    'remote': "Denna lista finns sedan tidigare redan inlagd."
                }
            },
            'submitHandler': function (form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	SchedulePrint : function(params) {
        Date.format = 'yyyy-mm-dd';
        startdate = params['startdate'];
        $('.datepick').datePicker({ startDate: startdate, clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthan': [$('#StartDate')]
                }
            },
            'messages': {
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                }
            },
            'submitHandler': function(form) {
	            $(form).attr('target','mcssprint');
	            window.open('about:blank','mcssprint','width=925,height=700,toolbar=no');
	            form.submit();
            }
        });
	},
    PreparePrint : function(params) {
        Date.format = 'yyyy-mm-dd';
        startdate = params['startdate'];
        $('.datepick').datePicker({ startDate: startdate, clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthan': [$('#StartDate')]
                }
            },
            'messages': {
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                }
            },
            'submitHandler': function(form) {
	            $(form).attr('target','mcssprint');
	            window.open('about:blank','mcssprint','width=925,height=700,toolbar=no');
	            form.submit();
            }
        });
	},
	SequenceCreate : function(params) {
        $('.checkbox > input[type="checkbox"]').click(function () {
            $('#TestTimes').valid();
        });

        Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ clickInput: true });
        $('.btn-mdatepick').datePicker({
            clickInput: true,
            createButton: false,
            displayClose: true,
            closeOnSelect: false,
            selectMultiple: true
        }).bind('click', function () {
            $(this).dpDisplay();
            this.blur();
            return false;
        }).bind('dpClosed', function (e, selectedDates) {
            var datestring = '';
            for (var i = 0; i < selectedDates.length; i++) {
            var d = new Date(selectedDates[i]);
                datestring += ',' + d.getFullYear() + '-' + (((d.getMonth() + 1) < 10) ? '0' : '') + (d.getMonth() + 1) + '-' + ((d.getDate() < 10 ? '0' : '')) + d.getDate();
            }
            datestring = (datestring.length > 0) ? datestring.substring(1) : datestring;
            $('#activity-freq-days').val(datestring)
        });

        var initWithValues = $('#activity-freq-days').val();
        if (initWithValues.length > 0) {
            var dates = initWithValues.split(',');
            for (var i = 0; i < dates.length; i++) {
                $('.btn-mdatepick').dpSetSelected(dates[0]);
            }
        }

        $('.std-form form').validate({
            ignore: [],
            'rules': {
                'Name': 'required',
                'Interval': {
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'StartDate': {
                    'date': true,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0 && $('#activity-freq-days').val().length == 0;
                    },
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'dategreaterthan': [$('#StartDate')]
                },
                'TestTimes': {
                    'required': function () {
                        var hasChecked = $('#TestTimes').parent().find('input:checked').length == 0;
                        return $('#activity-type-1:checked').length > 0 && hasChecked;
                    }
                },
                'RangeInMinutesBefore': {
                    'min': 0,
                    'max': 99999,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'RangeInMinutesAfter': {
                    'min': 0,
                    'max': 99999,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'ReminderInMinutesBefore': {
                    'min': 0,
                    'max': 120,
                    'required': function () {
                        return $('#Reminder:checked').length > 0;
                    }
                },
                'ReminderRecipient': {
                    'required': function () {
                        return $('#Reminder:checked').length > 0;
                    }
                },
                'OnNeedBasisStartDate': {
                    'date': true,
                    'required': function () {
                        return $('#activity-type-2:checked').length > 0;
                    },
                    'datelessthan': [$('#OnNeedBasisEndDate')]
                },
                'OnNeedBasisEndDate': {
                    'date': true,
                    'dategreaterthan': [$('#OnNeedBasisStartDate')]
                },
                'Inventory': {
                    'required': function () {
                        return $('#inventory-type-2:checked').length > 0;
                    }
                }
            },
            'messages': {
                'Name': "Insats måste fyllas i.",
                'Interval': "En frekvens måste anges.",
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'TestTimes': "Klockslag måste anges.",
                'RangeInMinutesBefore': {
                    'required': "Ges inom måste fyllas i.",
                    'min': "Måste vara mellan 0-99999.",
                    'max': "Måste vara mellan 0-99999."
                },
                'RangeInMinutesAfter': {
                    'required': "Ges inom måste fyllas i.",
                    'min': "Måste vara mellan 0-99999.",
                    'max': "Måste vara mellan 0-99999."
                },
                'ReminderInMinutesBefore': {
                    'required': "Tid för påminnelse måste fyllas i.",
                    'min': "Måste vara mellan 0-120.",
                    'max': "Måste vara mellan 0-120."
                },
                'ReminderRecipient': "Mottagare måste anges.",
                'OnNeedBasisStartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'OnNeedBasisEndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'Inventory': {
                    'required': "Saldo måste väljas",
                 }
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	SequenceEdit : function(params) {
        $('.checkbox > input[type="checkbox"]').click(function () {
            $('#TestTimes').valid();
        });

        Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ clickInput: true });
        $('.btn-mdatepick').datePicker({
            clickInput: true,
            createButton: false,
            displayClose: true,
            closeOnSelect: false,
            selectMultiple: true
        }).bind('click', function () {
            $(this).dpDisplay();
            this.blur();
            return false;
        }).bind('dpClosed',
			function (e, selectedDates) {
			    var datestring = '';
			    for (var i = 0; i < selectedDates.length; i++) {
			        var d = new Date(selectedDates[i]);
			        datestring += ',' + d.getFullYear() + '-' + (((d.getMonth() + 1) < 10) ? '0' : '') + (d.getMonth() + 1) + '-' + ((d.getDate() < 10 ? '0' : '')) + d.getDate();
			    }
			    datestring = (datestring.length > 0) ? datestring.substring(1) : datestring;
			    $('#activity-freq-days').val(datestring)
			}
		);

        var initWithValues = $('#activity-freq-days').val();
        if (initWithValues.length > 0) {
            var dates = initWithValues.split(',');
            for (var i = 0; i < dates.length; i++) {
                $('.btn-mdatepick').dpSetSelected(dates[i]);
            }
        }

        $('.std-form form').validate({
            ignore: [],
            'rules': {
                'Name': 'required',
                'Inventory': 'required',
                'Interval': {
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'StartDate': {
                    'date': true,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0 && $('#activity-freq-days').val().length == 0;
                    },
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'dategreaterthan': [$('#StartDate')]
                },
                'TestTimes': {
                    'required': function () {
                        var hasChecked = $('#TestTimes').parent().find('input:checked').length == 0;
                        return $('#activity-type-1:checked').length > 0 && hasChecked;
                    }
                },
                'RangeInMinutesBefore': {
                    'min': 0,
                    'max': 99999,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'RangeInMinutesAfter': {
                    'min': 0,
                    'max': 99999,
                    'required': function () {
                        return $('#activity-type-1:checked').length > 0;
                    }
                },
                'ReminderInMinutesBefore': {
                    'min': 0,
                    'max': 120,
                    'required': function () {
                        return $('#Reminder:checked').length > 0;
                    }
                },
                'ReminderRecipient': {
                    'required': function () {
                        return $('#Reminder:checked').length > 0;
                    }
                },
                'OnNeedBasisStartDate': {
                    'date': true,
                    'required': function () {
                        return $('#activity-type-2:checked').length > 0;
                    },
                    'datelessthan': [$('#OnNeedBasisEndDate')]
                },
                'OnNeedBasisEndDate': {
                    'date': true,
                    'dategreaterthan': [$('#OnNeedBasisStartDate')]
                }
            },
            'messages': {
                'Name': "Insats måste fyllas i.",
                'Inventory': "Saldo måste väljas.",
                'Interval': "En frekvens måste anges.",
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'TestTimes': "Klockslag måste anges.",
                'RangeInMinutesBefore': {
                    'required': "Ges inom måste fyllas i.",
                    'min': "Måste vara mellan 0-59.",
                    'max': "Måste vara mellan 0-59."
                },
                'RangeInMinutesAfter': {
                    'required': "Ges inom måste fyllas i.",
                    'min': "Måste vara mellan 0-59.",
                    'max': "Måste vara mellan 0-59."
                },
                'ReminderInMinutesBefore': {
                    'required': "Tid för påminnelse måste fyllas i.",
                    'min': "Måste vara mellan 0-59.",
                    'max': "Måste vara mellan 0-59."
                },
                'ReminderRecipient': "Mottagare måste anges.",
                'OnNeedBasisStartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'OnNeedBasisEndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                }
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	EventCreate : function(params) {
	    Date.format = 'yyyy-mm-dd';
	    $('#AllDay').change(function () {
            if ($(this).prop('checked')) {
                $('#StartTime').val('00:00');
                $('#EndTime').val('23:59');
                $(".std-form form").valid();
            }
        });
        $('.datepick').datePicker({ clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'Interval': { 'interval': [$('#StartDate'), $('#EndDate'), $('#Interval'), $('#IntervalFactor')] },
                'Category' : 'required',
                'NewCategory' : {
                    'required' : function () {
                        return $('#Category').val() == 'new';
                    }
                },
                'Description': 'required',
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthanorequal': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthanorequal': [$('#StartDate')]
                },
                'StartTime': {
                    'required': function () {
                        return $('#AllDay:checked').length == 0;
                    },
                    'time': function () {
                        return $('#AllDay:checked').length == 0;
                    },
                },
                'EndTime': {
                    'required': function () {
                        return $('#AllDay:checked').length == 0;
                    },
                    'time' : function () {
                        return $('#AllDay:checked').length == 0;
                    },
                }
            },
            'messages': {
                'Interval' : "Upprepnings-intervallet kan inte vara kortare än aktiviten",
                'Category' : "Kategori måste väljas",
                'NewCategory' : "Namn måste anges på ny kategori",
                'Description': "Aktiviteten måste ha en beskrivning.",
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthanorequal': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthanorequal': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'StartTime': {
                    'required': "Tid måste fyllas i.",
                    'time': "Tid måste fyllas i med fyra siffror och kolon, t.ex. 14:30."
                },
                'EndTime': {
                    'required': "Tid måste fyllas i.",
                    'time': "Tid måste fyllas i med fyra siffror och kolon, t.ex. 14:30."
                }
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	AccountCreate : function(params) {
        var id = params['id'];
        var uidUrl = params['uidUrl'], 
            taxonUrl = params['taxonUrl'],
            pwdUrl = '';
        $('.std-form form').validate({
            'rules': {
                'FirstName': 'required',
                'LastName': 'required',
                'PersonalIdentityNumber': {
                    'required': true,
                    'socialsecuritynumber': 'socialsecuritynumber',
                    'remote': {
                        'url': uidUrl,
                        'type': "post",
                        'data': {
                            'id': id, 
                            'uniqueIdentifier': $('#UniqueIdentifier').val()
                        }
                    }
                },
                'Email': {
                    'required': true,
                    'email': true
                },
                'Password': {
                    'required': true,
                    'digits': true,
                    'minlength': 4,
                    'maxlength': 7
                    /*'remote': {
                        'url': pwdUrl,
                        'type': "post",
                        'data': {
                            'password': $('#Password').val()
                        }
                    }*/
                },
                'TitleRole': 'required',
                'Taxon': {
                    'required': true,
                    'remote': {
                        'url': taxonUrl,
                        'type': "post",
                        'data': {
                            'taxon': $('#Taxon').val()
                        }
                    }
                }
            },
            'messages': {
                'FirstName': "Förnamn måste fyllas i.",
                'LastName': "Efternamn måste fyllas i.",
                'PersonalIdentityNumber': {
                    'required': "Personnummer måste fyllas i.",
                    'remote': "Personnumret finns sedan tidigare redan i MCSS.",
                    'socialsecuritynumber': "Personnummer måste fyllas i med tolv siffror och bindestreck, t. ex. 19010101-0001."
                },
                'Email': {
                    'required': "E-postadress måste fyllas i.",
                    'email': "E-postadress måste anges i korrekt format, t. ex. namn.efternamn@foretag.se."
                },
                'Password': {
                    'required': "Ett Lösenord måste fyllas i.",
                    'digits': "Ange ett fyrsiffrigt lösenord.",
                    'maxlength': "Maximalt antal tecken är 7 siffror.",
                    'minlength': "Minimilängden är fyra siffror.",
                    'remote': "Lösenordet används redan av en annan medarbetare."
                },
                'TitleRole': "En titel måste väljas.",
                'Taxon': "Adress måste väljas.",
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	DelegationUpdate : function(params) {
		Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthan': [$('#StartDate')]
                }
            },
            'messages': {
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                }
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	DelegationCreate : function(params) {
		$('.delegdialog .multisel select').change(function () {
            if ($(this).val() != '') {
                $(this).parent().append('<span class="person">' + $(this).find(':selected').text() + ' <a href="#" title="Ta bort">Ta bort</a><input type="hidden" name="Patients" value="' + $(this).find(':selected').val() + '"/></span>');
                $('#TestPatients').valid();
            }
        });
        $('.delegdialog .multisel .person a').live('click', function () {
            $(this).parent().slideUp(100).remove();
            return false;
        });

        $('.deleglist li').hide();
        $('.catlist a').click(function () {
            var t = $(this);
            $('.deleglist').hide();
            $('.deleglist li').hide();
            t.parent().parent().find('.sel').removeClass('sel');
            t.parent().addClass('sel');
            $('.deleglist .' + t.attr('class')).show();
            $('.deleglist').show();
            return false;
        });

        $('.deleglist li input').click(function () {
            if ($(this).is(':checked')) {
                $('#TestDelegations').valid();
            }
        });

        Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ clickInput: true });
        $('.std-form form').validate({
            'ignore':"",
            'rules': {
                'Delegation': {
                    'required': function () {
                        return $('#activity-type-2:checked').length > 0;
                    }
                },
                'DelegationType': {
                    'required': function () {
                        return $('#activity-type-2:checked').length > 0;
                    }
                },
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthan': [$('#StartDate')]
                },
                'TestDelegations': {
                    'required': function () {
                        var isRequired = $('#activity-type-1:checked').length > 0;                    
                        if (isRequired) {                  
                            isRequired = !($('.deleglist li input:checked').length > $('.deleglist li input:disabled').length);
                        }
                        return isRequired;
                    }
                }/*,
                'TestPatients': {
                    'required': function () {
                        return $('.person').length == 0;
                    }
                }*/
            },
            'messages': {
                'Delegation': "Delegering måste väljas.",
                'DelegationType': "Delegeringstyp måste väljas.",
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'TestDelegations': "Delegering måste väljas.",
                'TestPatients': "Boende måste väljas."
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	DelegationEdit : function(params) {
		$('.delegdialog .multisel select').change(function () {
            if ($(this).val() != '') {
                $(this).parent().append('<span class="person">' + $(this).find(':selected').text() + ' <a href="#" title="Ta bort">Ta bort</a><input type="hidden" name="Patients" value="' + $(this).find(':selected').val() + '"/></span>');
                $('#TestPatients').valid();
            }
        });
        $('.delegdialog .multisel .person a').live('click', function () {
            $(this).parent().slideUp(100).remove();
            return false;
        });

        Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'StartDate': {
                    'date': true,
                    'required': true,
                    'datelessthan': [$('#EndDate')]
                },
                'EndDate': {
                    'date': true,
                    'required': true,
                    'dategreaterthan': [$('#StartDate')]
                },
                'TestPatients': {
                    'required': function () {
                        return $('.person').length == 0;
                    }
                }
            },
            'messages': {
                'StartDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'datelessthan': "Startdatum måste vara ett tidigare datum är slutdatum."
                },
                'EndDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21.",
                    'dategreaterthan': "Slutdatum måste vara ett senare datum är startdatum."
                },
                'TestPatients': "Boende måste väljas."
            },
            'submitHandler': function(form) {
	            $(form).find('input[type=submit]').attr('disabled','disabled');
	            form.submit();
            }
        });
	},
	AddToStock: function (params) {
		$('.std-form form').validate({
            'rules': {
                'Value': {
                    'min':1,
                    'max': 99999,
                    'required': true
                }
            },
            'messages': {
                'Value': {
                    'required': "Mängd måste fyllas i.",
                    'min': "Mängd måste vara ett numeriskt värde och vara mindre än 1.",
                    'max': "Mängd får ej vara större än 99999."
                }
            }
        });
	},
	RecalculateStock : function(params) {
		$('.std-form form').validate({
            'rules': {
                'Value': {
                    'min': 0,
                    'max': 99999,
                    'required': true
                }
            },
            'messages': {
                'Value': {
                    'required': "Mängd måste fyllas i.",
                    'min': "Mängd måste vara ett numeriskt värde och vara mindre än 0.",
                    'max': "Mängd får ej vara större än 99999."
                }
            }
        });
	},
    KnowledgeTestEdit: function(params) {
        Date.format = 'yyyy-mm-dd';
        $('.datepick').datePicker({ startDate: '1970-01-01', clickInput: true });
    },
    KnowledgeTestAdd : function(params) {
        Date.format = 'yyyy-mm-dd';
        startdate = params['startdate'];
        $('.datepick').datePicker({ startDate: '1970-01-01', endDate: new Date(), clickInput: true });
        $('.std-form form').validate({
            'rules': {
                'Name': {
                    'required': true
                },
                'CompletedDate': {
                    'date': true,
                    'required': true
                }
            },
            'messages': {
                'Name': {
                    'required': "Kunskapstest måste fyllas i."
                },
                'CompletedDate': {
                    'required': "Datum måste fyllas i.",
                    'date': "Datum måste fyllas i med åtta siffror och bindestreck, t. ex. 2012-12-21."
                }
            }
        });
    },
    InventoryCreateUpdate: function (params) {
        $('.std-form form').validate({
            'rules': {
                'Name': 'required'
            },
            'messages': {
                'Name': {
                    'required': "Namn måste fyllas i."
                }
            },
            'submitHandler': function (form) {
                $(form).find('input[type=submit]').attr('disabled', 'disabled');
                form.submit();
            }
        });
    },
};

mcss.validation.applyRules = function(valclass,valparams) {
	mcss.validation[valclass](valparams);
};