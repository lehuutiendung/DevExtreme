<template>
    <div class="dx-field-value">
        <DxDropDownBox
          ref="dropdownBox"
          :value="treeBoxValue"
          :show-clear-button="false"
          :data-source="treeDataSource"
          :tabIndex="tabIndex"
          value-expr="DepartmentName"
          display-expr="DepartmentName"
          @value-changed="syncTreeViewSelection($event)"
        >
  
          <!-- <div slot="fieldTemplate" slot-scope="{ data }">
                <div class="custom-item">
                    <div class="product-name">
                        <DxTextBox
                            :value="data"
                            :read-only="true">
                            {{ data }}
                        </DxTextBox>
                    </div>
                </div>
            </div> -->
          <template #content="{ }">
            <DxTreeView
              ref="treeView"
              :data-source="treeDataSource"
              :select-by-click="true"
              :searchEnabled="true"
              placeholder="Tìm kiếm"
              noDataText="Không có dữ liệu"
              data-structure="plain"
              key-expr="DepartmentId"
              parent-id-expr="ParentId"
              root-value="00000000-0000-0000-0000-000000000000"
              selection-mode="multiple"
              show-check-boxes-mode="normal"
              display-expr="DepartmentName"
              @content-ready="contentReady"
              @item-selection-changed="treeView_itemSelectionChanged($event)"
            />
          </template>
        </DxDropDownBox>
    </div>
</template>
<script>
import DxDropDownBox from 'devextreme-vue/drop-down-box';
import DxTreeView from 'devextreme-vue/tree-view';
export default {
    name: "DropdownMultiple",
    components: {
        DxDropDownBox,
        DxTreeView,
    },
    props:{
        treeDataSource:{
          type: Array,
          default(){
            return [];
          }
        },

        //Dữ liệu Đơn vị áp dụng của bản ghi (xem chi tiết/sửa)
        value:[String, Array],
        
        //tabindex
        tabIndex: {
            type: Number,
            default(){
                return 1;
            }
        },
    },
    data() {
        return {
            treeBoxValue: this.value,
            isFirst: true
        };
    },
    
    methods: {
        /**
         * @Description Xử lý sự kiện khi item được click và bind dữ liệu
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        syncTreeViewSelection() {
          if (!this.$refs.treeView) return;
          if (!this.treeBoxValue || this.treeBoxValue.length == 0) {
            this.$refs.treeView.instance.unselectAll();
          } else {
            for(var i = 0; i <this.treeBoxValue.length; i++){
              var id = this.treeDataSource.find(x => x.DepartmentName == this.treeBoxValue[i]);
              this.$refs.treeView.instance.selectItem(id.DepartmentId);
            }
          }
        },
        contentReady(){
          if(this.isFirst){
            this.$refs.treeView.instance.unselectAll();
            this.syncTreeViewSelection();
            this.isFirst = false;
          }
        },
        /**
         * @Description Xử lý sự kiện cho item được trọn trong tree data
         * @Date 18/9/2021
         * @CreatedBy LHTDung
         */
        treeView_itemSelectionChanged(e) {
          this.$emit('updateValueDropdownMulti', e.component.getSelectedNodes());
          let nodes = e.component.getSelectedNodes();
          this.treeBoxValue = nodes.map(x => x.text)
          // let arrayName = [];
          // // Mảng arrayName chứa các tên đơn vị áp dụng được chọn
          // nodes.forEach(node => {
          //   arrayName.push(node.text);
          // });
          // console.log(arrayName);
          // // Gán lại mảng cho treeBoxValue (phụ thuộc vào selectItem)
          // this.treeBoxValue = arrayName;
        },
    },
}
</script>
<style scoped>
    @import "../../../css/base/dropdown-multiple/base-dropdown-multiple.css";
</style>