<template>
<!------------------------------------------------ COMBOBOX TAG ----------------------------------------------------------->
    <div class="wrap-combobox">
        <div class="dx-field">
            <div class="dx-field-value">
                <DxTagBox
                :items="dataSource"
                :value="arrayValue"
                :show-selection-controls="true"
                :search-enabled="true"
                :placeholder="placeholder"
                :tabIndex="tabIndex"
                @value-changed="onValueChanged"
                />
            </div>
            <div class="icon-36 wrap-icon-combobox">
                <div class="icon-20 icon-combobox">

                </div>
            </div>
        </div>
    </div>
    
</template>

<script>
import { EventBus } from "../../../main"

import DxTagBox from 'devextreme-vue/tag-box';
export default {
    components: {
        DxTagBox,
    },
    props:{
        placeholder:{
            type: String,
            default(){
                return "";
            }
        },

        //Danh sách chứa tất cả bản ghi ( Vị trí || Nhân viên )
        listObjectData:{
            type: Array,
            deafult(){
                return [];
            }
        },

        //Loại combobox để phân biệt Combobox tag của Vị trí và Nhân viên
        nameCombobox:{
            type: String,
            default(){
                return '';
            }
        },
        
        //Dữ liệu Vị trí áp dụng/Nhân viên áp dụng của bản ghi (xem chi tiết | sửa) 
        value:{
          type: String,
          default(){
            return '';
          }
        },
        
        //tabindex
        tabIndex: {
            type: Number,
            default(){
                return 1;
            }
        },
    },
    data(){
        return {
            dataSource: [],   //Danh sách chứa tên của các vị trí
            arrayValue: [],   //Danh sách chứa dữ liệu bind lên DxTagBox
            itemChecked: [],  //Danh sách chứa các item được chọn
        }
    },
    created() {
        // Tách chuỗi thành các phần tử và đẩy vào arrayValue để tạo thành các tag
        if(this.value != ''){
            this.arrayValue = this.value;
            this.arrayValue = this.arrayValue.split('; ');
        }
        /**
         * @description Lưu data vào danh sách dataSource
         * @created LHTDung
         * @date 22/09/2021
         */
        if(this.listObjectData){
            if(this.nameCombobox == this.$resourceVn.POSITION){
                this.listObjectData.forEach(item => {
                    this.dataSource.push(item.PositionName);
                });
            }
            if(this.nameCombobox == this.$resourceVn.EMPLOYEE){
                this.listObjectData.forEach(item => {
                    this.dataSource.push(item.FullName);
                });
            }
        }
    },
    methods: {
        /**
         * @description Xử lý sự kiện thay đổi giá trị của DxTagBox
         * @created LHTDung
         * @date 22/09/2021
         */
        onValueChanged(e) {
            // Event handling commands go here
            this.itemChecked = e.value;
            EventBus.$emit('updateValueCombobox', this.nameCombobox, this.itemChecked);
        }
    },
}
</script>
<style scoped>
    @import "../../../css/base/combobox/base-combobox.css";
</style>